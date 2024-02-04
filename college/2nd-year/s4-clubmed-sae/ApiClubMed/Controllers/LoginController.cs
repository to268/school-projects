using ApiClubMed.Models;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiClubMed.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataRepository<Client> dataRepository;
        private List<User> lesUsers = new List<User>();


        private User admin = new User
        {
            Email = "accesadmin@gmail.com",
            Password = "gfdffFDGDF4154d561SDFGD45111sd1FDS153",
            UserRole = "Admin"
        };

        public LoginController(IConfiguration config, IDataRepository<Client> dataRepo)
        {
            _config = config;
            dataRepository = dataRepo;

            var lesClients = dataRepository.GetAllAsync().Result.Value.ToList();

            lesUsers.Clear();

            foreach (Client client in lesClients)
            {
                lesUsers.Add(new User
                {
                    Email = client.Email,
                    Password = client.Password,
                    UserRole = "Client"
                });
            }
            lesUsers.Add(admin);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateClient(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        private User AuthenticateClient(User user)
        {
            return lesUsers.SingleOrDefault(x => x.Email.ToUpper() == user.Email.ToUpper() && x.Password == user.Password);
        }
        private string GenerateJwtToken(User userInfo)
        {
            var securityKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(480),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

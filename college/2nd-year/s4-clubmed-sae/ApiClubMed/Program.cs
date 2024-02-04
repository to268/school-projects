using ApiClubMed.Models;
using ApiClubMed.Models.DataManager;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace ApiClubMed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("https://apiclubmed.azurewebsites.net")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                doc =>
                {
                    var xmlFile = Path.ChangeExtension(typeof(Program).Assembly.Location, ".xml");
                    doc.IncludeXmlComments(xmlFile);
                }
            );
            builder.Services.AddDbContext<ClubMedDBContexts>(Options => Options.UseNpgsql(builder.Configuration.GetConnectionString("ClubMedDbContextRemote")));
            builder.Services.AddScoped<IDataRepository<Resort>, ResortManager>();
            builder.Services.AddScoped<IDataRepository<ActiviteCarte>, ActiviteCarteManager>();
            builder.Services.AddScoped<IDataRepository<ActiviteEnfantCarte>, ActiviteEnfantCarteManager>();
            builder.Services.AddScoped<IDataRepository<ActiviteEnfantIncluse>, ActiviteEnfantIncluseManager>();
            builder.Services.AddScoped<IDataRepository<ActiviteIncluse>, ActiviteIncluseManager>();
            builder.Services.AddScoped<IDataRepository<DomaineSkiable>, DomaineSkiableManager>();
            builder.Services.AddScoped<IDataRepository<Client>, ClientManager>();
            builder.Services.AddScoped<IDataRepository<CarteBanquaire>, CarteBanquaireManager>();
            builder.Services.AddScoped<IDataRepository<Reservation>, ReservationManager>();
            builder.Services.AddScoped<IDataRepository<Localisation>, LocalisationManager>();
            builder.Services.AddScoped<IDataRepository<RegroupementClub>, RegroupementClubManager>();
            builder.Services.AddScoped<IDataRepository<Payement>, PayementManager>();
            builder.Services.AddScoped<IDataRepository<Pays>, PaysManager>();
            builder.Services.AddScoped<IDataRepository<SousLocalisation>, SousLocalisationManager>();
            builder.Services.AddScoped<IDataRepository<TrancheAge>, TrancheAgeManager>();
            builder.Services.AddScoped<IDataRepository<TypeActivite>, TypeActiviteManager>();
            builder.Services.AddScoped<IDataRepository<Transport>, TransportManager>();
            builder.Services.AddScoped<IDataRepository<TypeClub>, TypeClubManager>();
            builder.Services.AddScoped<IDataRepository<Bar>, BarManager>();
            builder.Services.AddScoped<IDataRepository<Restaurant>, RestaurantManager>();
            builder.Services.AddScoped<IDataRepository<CategorieTypeChambre>, CategorieTypeChambreManager>();
            builder.Services.AddScoped<IDataRepository<TypeChambre>, TypeChambreManager>();
            builder.Services.AddScoped<IDataRepository<PointFort>, PointFortManager>();
            builder.Services.AddScoped<IDataRepository<Commodite>, CommoditeManager>();
            builder.Services.AddScoped<IDataRepository<Video>, VideoManager>();
            builder.Services.AddScoped<IDataRepository<Photo>, PhotoManager>();
            builder.Services.AddScoped<IDataRepository<Avis>, AvisManager>();
            builder.Services.AddScoped<IDataRepository<AutreParticipant>, AutreParticipantManager>();
            builder.Services.AddScoped<IDataRepository<Civilite>, CiviliteManager>();

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Client, Policies.ClientPolicy());
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.ClientOrAdmin, Policies.ClientOrAdminPolicy());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}
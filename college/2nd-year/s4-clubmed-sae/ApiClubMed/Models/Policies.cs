using Microsoft.AspNetCore.Authorization;

namespace ApiClubMed.Models
{
    public class Policies
    {
        public const string Client = "Client";
        public const string Admin = "Admin";
        public const string ClientOrAdmin = "ClientOrAdmin";

        public static AuthorizationPolicy ClientPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Client).Build();
        }
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
        public static AuthorizationPolicy ClientOrAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin, Client).Build();
        }
    }
}


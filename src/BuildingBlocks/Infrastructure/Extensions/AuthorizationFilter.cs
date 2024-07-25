using Hangfire.Dashboard;

namespace Infrastructure.Extensions
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}

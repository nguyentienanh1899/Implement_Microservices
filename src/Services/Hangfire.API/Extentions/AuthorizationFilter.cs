using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Hangfire.API.Extentions
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }
}

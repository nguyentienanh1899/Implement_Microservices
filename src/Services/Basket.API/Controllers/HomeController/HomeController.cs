using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.HomeController
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");        
        }
    }
}

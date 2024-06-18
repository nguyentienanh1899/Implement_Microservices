using Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Identity;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenSerivce;
        public TokenController(ITokenService tokenSerivce)
        {
            _tokenSerivce = tokenSerivce;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetToken()
        {
            var result = _tokenSerivce.GetToken(new TokenRequest());
            return Ok(result);
        }
    }
}

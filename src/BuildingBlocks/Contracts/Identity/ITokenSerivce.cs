using Shared.DTOs.Identity;

namespace Contracts.Identity
{
    public interface ITokenService
    {
        TokenRespone GetToken(TokenRequest request);
    }
}

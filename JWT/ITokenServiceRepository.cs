
using Presentation_Layer.Responses.Auth;

namespace practice.Services;

public interface ITokenService
{
    string CreateAccessToken(AuthResponse authResponse);
}
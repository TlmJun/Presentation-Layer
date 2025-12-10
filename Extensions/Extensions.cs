using System.Security.Authentication;
using static practice.Services.TokenService;

namespace practice.Extensions;

public static class HttpContextExtensions
{
    public static int TryGetUserId(this HttpContext httpContext)
    {
        var studentIdValue = httpContext.User.Claims.FirstOrDefault(c => c.Type == TestingPlatformClaimTypes.StudentId)?.Value;

        if (!int.TryParse(studentIdValue, out var studentId))
        {
            throw new AuthenticationException("Данные о пользователе пусты.");
        }

        return studentId;
    }
}


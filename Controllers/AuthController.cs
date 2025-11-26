using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice.Requests.Auth;
using practice.Responses.Auth;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;

namespace practice.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class AuthController(IAuthRepository authRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Authorize([FromBody] AuthRequest auth)
    {
        var userLoginDto = mapper.Map<UserLoginDto>(auth);
        var user = await authRepository.AuthorizeUser(userLoginDto);
        var response = mapper.Map<AuthResponse>(user);

        return Ok(response);
    }
}


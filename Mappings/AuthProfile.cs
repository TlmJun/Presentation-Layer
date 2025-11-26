using AutoMapper;
using practice.Requests.Auth;
using practice.Responses.Auth;
using TestingPlatform.Application.Dtos;

namespace practice.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<AuthRequest, UserLoginDto>();
        CreateMap<UserDto, AuthResponse>();
    }
}
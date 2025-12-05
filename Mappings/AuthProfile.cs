using AutoMapper;
using practice.Requests.Auth;
using Presentation_Layer.Responses.Auth;
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
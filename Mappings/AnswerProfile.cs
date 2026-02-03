using AutoMapper;
using practice.Requests.Answer;
using TestingPlatform.Application.Dtos;

namespace practice.Mappings;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<CreateAnswerRequest, AnswerDto>();
        CreateMap<UpdateAnswerRequest, AnswerDto>();
    }
}
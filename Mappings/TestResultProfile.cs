using AutoMapper;
using practice.Responses.TestResult;
using TestingPlatform.Application.Dtos;

namespace practice.Mappings;

public class TestResultProfile : Profile
{
    public TestResultProfile()
    {
        CreateMap<TestResultDto, TestResultResponse>();
    }
}


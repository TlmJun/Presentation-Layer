using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice.Extensions;
using practice.Responses.TestResult;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;

namespace practice.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class TestResultsController(ITestResultsRepository testResultsRepository, IMapper mapper) : ControllerBase
{
    [HttpGet("manage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<IEnumerable<TestResultDto>>> GetAllTestResultsForManager()
    {
        var testResultDtos = await testResultsRepository.GetAllAsync();
        var result = mapper.Map<IEnumerable<TestResultResponse>>(testResultDtos);

        return Ok(result);
    }

    [HttpGet("student")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Roles")]
    public async Task<ActionResult<TestResultDto>> GetTestResultsForStudent()
    {
        var studentId = HttpContext.TryGetUserId();
        var testResultDtos = await testResultsRepository.GetByStudentIdAsync(studentId);
        var result = mapper.Map<IEnumerable<TestResultResponse>>(testResultDtos);

        return Ok(result);
    }
}


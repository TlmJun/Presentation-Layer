using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;
using practice.Responses.Test;
using practice.Requests.Test;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace practice.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TestController(ITestRepository testRepository, IMapper mapper) : ControllerBase
{

    [HttpGet("manage")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(IEnumerable<TestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTestsForManager([FromQuery] bool? isPublic, [FromQuery] List<int> groupIds, [FromQuery] List<int> studentIds)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var tests = await testRepository.GetAllAsync(isPublic, groupIds, studentIds);

        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }

    [HttpGet]
    public async Task<IActionResult> GetTests()
    {
        var tests = await testRepository.GetAllAsync(null, new List<int>(), new List<int>());
        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }
    [HttpGet(("{Id:int}"))]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        var id = await testRepository.GetByIdAsync(Id);
        return Ok(mapper.Map<IEnumerable<TestResponse>>(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest test)
    {
        var testDto = new TestDto()
        {
            Title = test.Title,
            Description = test.Description,
            IsRepeatable = test.IsRepeatable,
            Type = test.Type,
            PublishedAt = test.PublishedAt,
            Deadline = test.Deadline,
            DurationMinutes = test.DurationMinutes,
            IsPublic = test.IsPublic,
            PassingScore = test.PassingScore,
            MaxAttempts = test.MaxAttempts
        };
        var testId = await testRepository.CreateAsync(testDto);
        return StatusCode(StatusCodes.Status201Created, new { Id = testId });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateTest([FromBody] UpdateTestRequest test)
    {
        await testRepository.UpdateAsync(mapper.Map<TestDto>(test));
        return NoContent();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteTest(int id)
    {
        await testRepository.DeleteAsync(id);
        return NoContent();
    }

}


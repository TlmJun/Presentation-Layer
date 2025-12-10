using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using practice.Responses.Question;
using practice.Requests.Question;
using Microsoft.AspNetCore.Authorization;

namespace practice.Controllers;
[ApiController]
[Authorize(Roles = "Manager")]
[Route("api/[controller]")]
public class QuestionController(IQuestionRepository questionRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var questions = await questionRepository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<QuestionResponse>>(questions));
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var question = await questionRepository.GetByIdAsync(id);
        return Ok(mapper.Map<QuestionResponse>(question));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionRequest  question)
    {
        var questionDto = new QuestionDto()
        {
            Text = question.Text,
            Number = question.Number,
            Description = question.Description,
            AnswerType = question.AnswerType,
            IsScoring = question.IsScoring,
            MaxScore = question.MaxScore,
            TestId = question.TestId
        };
        var questionid = await questionRepository.CreateAsync(questionDto);
        return StatusCode(StatusCodes.Status201Created, new {id = questionid });
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateQuestionRequest question)
    {
        await questionRepository.UpdateAsync(mapper.Map<QuestionDto>(question));
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await questionRepository.DeleteAsync(id);
        return NoContent();
    }
}

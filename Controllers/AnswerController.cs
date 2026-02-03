using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice.Requests.Answer;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;

namespace practice.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
[Authorize(Roles = "Manager")]
public class AnswerController(IAnswerRepository answerRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> CreateAnswer(CreateAnswerRequest answer)
    {
        var answerId = await answerRepository.CreateAsync(mapper.Map<AnswerDto>(answer));

        return StatusCode(StatusCodes.Status201Created, new { Id = answerId });
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAnswer(UpdateAnswerRequest answer)
    {
        await answerRepository.UpdateAsync(mapper.Map<AttemptDto>(answer));

        return NoContent();
    }
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteAnswer(int id)
    {
        await answerRepository.DeleteAsync(id);

        return NoContent();
    }
}


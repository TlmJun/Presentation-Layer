using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice.Requests.StudentAnswer;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;

namespace practice.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
[Authorize(Roles = "Student")]
public class StudentAnswersController(IStudentAnswerRepository studentAnswerRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateStudentAnswer(CreateStudentAnswerRequest answerRequest)
    {
        var dto = new UserAttemptAnswerDto
        {
            AttemptId = answerRequest.AttemptId,
            QuestionId = answerRequest.QuestionId,
            UserSelectedOptions = answerRequest.UserSelectedOptions,
            UserTextAnswers = answerRequest.UserTextAnswers
        };

        await studentAnswerRepository.CreateAsync(dto);

        return Ok();
    }
}


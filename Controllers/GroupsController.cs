using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice.Requests.Group;
using Presentation_Layer.Responses.Group;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;

namespace practice.Controllers;
/// <summary>
/// Контроллер управления группами.
/// </summary>
/// <param name="groupRepository">Репозиторий групп.</param>
/// <param name="mapper">Сервис маппинга.</param>
/// <remarks>
/// Доступ к операциям контроллера ограничен ролями:
/// Student -- доступ только к операциям чтения;
/// Manager -- полный доступ к управлению группами.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class GroupsController(IGroupRepository groupRepository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить список всех групп.
    /// </summary>
    /// <returns>Коллекция групп</returns>
    /// <response code="200">Список групп успешно получен</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="403">Недостаточно прав для выполнения операции</response>
    [HttpGet]
    [Authorize(Roles = "Student, Manager")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await groupRepository.GetAllAsync();

        return Ok(mapper.Map<IEnumerable<GroupResponse>>(groups));
    }

    /// <summary>
    /// Получить информацию о группе по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор группы</param>
    /// <returns>Данные группы</returns>
    /// <response code="200">Группа найдена и успешно возвращена</response>
    /// <response code="404">Группа с указанным идентификатором не найдена</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="403">Недостаточно прав для выполнения операции</response>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Student, Manager")]
    public async Task<IActionResult> GetGroupById(int id)
    {
        var group = await groupRepository.GetByIdAsync(id);

        return Ok(mapper.Map<GroupResponse>(group));
    }

    /// <summary>
    /// Создать новую группу.
    /// </summary>
    /// <remarks>
    /// Операция доступна только пользователям с ролью Manager.
    /// </remarks>
    /// <param name="group">Данные для создания группы</param>
    /// <returns>Идентификатор созданной группы</returns>
    /// <response code="201">Группа успешно создана</response>
    /// <response code="400">Некорректные входные данные</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="403">Недостаточно прав для выполнения операции</response>
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest group)
    {
        var id = await groupRepository.CreateAsync(mapper.Map<GroupDto>(group));

        return StatusCode(StatusCodes.Status201Created, new { Id = id });
    }

    /// <summary>
    /// Обновить данные группы.
    /// </summary>
    /// <remarks>
    /// Операция доступна только пользователям с ролью Manager.
    /// </remarks>
    /// <param name="group">Обновлённые данные учебной группы</param>
    /// <response code="204">Группа успешно обновлена</response>
    /// <response code="400">Некорректные входные данные</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="403">Недостаточно прав для выполнения операции</response>
    /// <response code="404">Группа не найдена</response>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest group)
    {
        await groupRepository.UpdateAsync(mapper.Map<GroupDto>(group));

        return NoContent();
    }

    /// <summary>
    /// Удалить группу.
    /// </summary>
    /// <remarks>
    /// Операция доступна только пользователям с ролью Manager.
    /// </remarks>
    /// <param name="id">Идентификатор группы</param>
    /// <response code="204">Группа успешно удалена</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="403">Недостаточно прав для выполнения операции</response>
    /// <response code="404">Группа не найдена</response>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        await groupRepository.DeleteAsync(id);

        return NoContent();
    }
}
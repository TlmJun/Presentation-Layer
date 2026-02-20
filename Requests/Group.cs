using System.ComponentModel.DataAnnotations;

namespace practice.Requests.Group;
/// <summary>
/// Модель обнолвения группы
/// </summary>
public class UpdateGroupRequest
{
    /// <summary>
    /// Идентификатор группы.
    /// </summary>
    /// <example>10</example>
    public int Id { get; set; }

    /// <summary>
    /// Название группы
    /// </summary>
    /// <example>КБ10</example>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор направления
    /// </summary>
    /// <example>3</example>
    public int DirectionId { get; set; }

    /// <summary>
    /// Идентфикатор курса
    /// </summary>
    /// <example>5</example>
    public int CourseId { get; set; }

    /// <summary>
    /// Идентификатор проекта
    /// </summary>
    /// <example>2</example>
    public int ProjectId { get; set; }
}


/// <summary>
/// Модель создания группы
/// </summary>
public class CreateGroupRequest
{
    /// <summary>
    /// Название группы
    /// </summary>
    /// <example>КБ10</example>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор направления
    /// </summary>
    /// <example>3</example>
    public int DirectionId { get; set; }

    /// <summary>
    /// Идентфикатор курса
    /// </summary>
    /// <example>5</example>
    public int CourseId { get; set; }

    /// <summary>
    /// Идентификатор проекта
    /// </summary>
    /// <example>2</example>
    public int ProjectId { get; set; }
}

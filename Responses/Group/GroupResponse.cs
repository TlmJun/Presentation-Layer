using TestingPlatform.Responses;
using Presentation_Layer.Responses.Course;
using Presentation_Layer.Responses.Direction;
using Presentation_Layer.Responses.Project;
namespace Presentation_Layer.Responses.Group;

/// <summary>
/// Модель группы
/// </summary>
/// <remarks>
/// Модель содержит информацию о связях учебной группы
/// с направлением обучения, курсом и проектом.
/// </remarks>
public class GroupResponse : BaseResponse
{
    /// <summary>
    /// Направление обучения, к которому относится группа.
    /// </summary>
    public DirectionResponse Direction { get; set; }

    /// <summary>
    /// Учебный курс, в рамках которого обучается группа.
    /// </summary>
    public CourseResponse Course { get; set; }

    /// <summary>
    /// Проект, к которому относится группа.
    /// </summary>
    public ProjectResponse Project { get; set; }
}
namespace Sample.Application.Dtos;

/// <summary>
/// Отдел
/// </summary>
public class DepartmentDto
{
    /// <summary>
    /// Название отдела
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Идентификаторы сотрудников
    /// </summary>
    public IEnumerable<int> PersonsIds { get; set; } = new List<int>();
}

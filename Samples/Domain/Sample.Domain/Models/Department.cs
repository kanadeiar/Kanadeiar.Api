namespace Sample.Domain.Models;

/// <summary>
/// Отдел
/// </summary>
public class Department : Entity
{
    /// <summary>
    /// Название отдела
    /// </summary>
    [StringLength(200, MinimumLength = 2)]
    public string Name { get; set; }

    /// <summary>
    /// Описание отдела
    /// </summary>
    [StringLength(400)]
    public string? Description { get; set; }

    /// <summary>
    /// Сотрудники в отделе
    /// </summary>
    public virtual IEnumerable<Person> Persons { get; set; } = new List<Person>();
}

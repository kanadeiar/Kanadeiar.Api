namespace Sample.Application.Dtos;

/// <summary>
/// Сотрудник
/// </summary>
public class PersonDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string SurName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    public DateTime BirthDay { get; set; } = DateTime.Today;

    /// <summary>
    /// Зарплата
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Рост
    /// </summary>
    public double Groth { get; set; }

    /// <summary>
    /// Отдел
    /// </summary>
    public int DepartmentId { get; set; }
}

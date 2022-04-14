namespace Sample.Domain.Models;

/// <summary>
/// Сотрудник
/// </summary>
public class Person : Entity
{
    /// <summary>
    /// Фамилия
    /// </summary>
    [StringLength(200, MinimumLength = 2)]
    public string SurName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [StringLength(200, MinimumLength = 2)]
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    [StringLength(200, MinimumLength = 2)]
    public string Patronymic { get; set; }

    /// <summary>
    /// день рождения
    /// </summary>
    public DateTime BirthDay { get; set; } = DateTime.Today;

    /// <summary>
    /// Зарплата
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }

    /// <summary>
    /// Рост
    /// </summary>
    public double Groth { get; set; }

    /// <summary>
    /// Отдел
    /// </summary>
    [Range(1, int.MaxValue)]
    public int DepartmentId { get; set; }
}

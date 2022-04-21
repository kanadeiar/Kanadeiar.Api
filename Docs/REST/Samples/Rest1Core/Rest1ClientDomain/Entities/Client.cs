using Rest1ClientDomain.Entities.Base;

namespace Rest1Core.Entities;

/// <summary>
/// Клиент
/// </summary>
public class Client : Entity
{
    /// <summary>
    /// Идентификатор пользователя системы авторизации
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [MaxLength(100)]
    public string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [MaxLength(100)]
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    [MaxLength(100)]
    public string Patronymic { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDay { get; set; }
}

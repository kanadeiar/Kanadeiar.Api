namespace Sample.Domain.Models.Base;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Базовый идентификатор
    /// </summary>
    [Key]
    //TODO база данных  : public int Id { get; private set; }
    public int Id { get; set; }
}

namespace Rest1ClientDomain.Entities.Base;

/// <summary>
/// База
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }
}

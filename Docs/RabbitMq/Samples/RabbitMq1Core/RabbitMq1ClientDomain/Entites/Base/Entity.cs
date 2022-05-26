namespace RabbitMq1ClientDomain.Entites.Base;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class Entity : IKndEntity<int>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary> 
    /// Маркер версии 
    /// </summary>
    [Timestamp]
    public byte[] RowVersion { get; set; }
}

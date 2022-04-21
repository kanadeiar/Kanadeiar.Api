using Kanadeiar.Core.Domain.Base;

namespace Rest1ClientDomain.Entities.Base;

/// <summary>
/// Базовая сущность
/// </summary>
abstract public class Entity : IKndEntity<int>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }
}

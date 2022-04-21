using System.ComponentModel.DataAnnotations;

namespace Kanadeiar.Api.Domain.Base;

/// <summary>
/// Базовая сущность
/// </summary>
abstract public class KndEntity<TId> : IKndEntity<TId>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public TId Id { get; set; }
}

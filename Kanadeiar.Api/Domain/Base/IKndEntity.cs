using System.ComponentModel.DataAnnotations;

namespace Kanadeiar.Api.Domain.Base;

/// <summary>
/// Базовая сущность
/// </summary>
public interface IKndEntity<TId> : IKndEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public TId Id { get; set; }
}

/// <summary>
/// Базовая сущность
/// </summary>
public interface IKndEntity
{
}

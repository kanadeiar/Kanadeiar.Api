﻿namespace Rest1Core.Entities.Base;

/// <summary>
/// База
/// </summary>
abstract public class Entity : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }
}

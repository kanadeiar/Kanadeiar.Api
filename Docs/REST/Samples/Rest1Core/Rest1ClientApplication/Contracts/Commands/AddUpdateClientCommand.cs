namespace Rest1ClientApplication.Contracts.Commands;

/// <summary>
/// Добавление или обновление сущности
/// </summary>
public class AddUpdateClientCommand : IRequest<int>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Данные модели
    /// </summary>
    public ClientDto Model { get; }

    /// <summary>
    /// Добавление или обновление сущности
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности или 0 - добавление модели</param>
    /// <param name="model">Новые данные</param>
    public AddUpdateClientCommand(int id, ClientDto model)
    {
        Id = id;
        Model = model;
    }
}

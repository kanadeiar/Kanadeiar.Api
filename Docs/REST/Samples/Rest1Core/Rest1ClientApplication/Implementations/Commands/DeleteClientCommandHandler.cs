namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик удаления элемента
/// </summary>
public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
{
    private readonly IClientRepository _repository;
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<AddUpdateClientCommandHandler> _logger;

    /// <summary> </summary>
    public DeleteClientCommandHandler(IClientRepository repository, IDbConnectionFactory connectionFactory, ILogger<AddUpdateClientCommandHandler> logger)
    {
        _repository = repository;
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    /// <summary>
    /// Обработка удаления элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Успешность удаления</returns>
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            using var db = _connectionFactory.CreateConnection();
            var sqlQuery = @"
DELETE FROM Clients 
WHERE Id = @id";
            var affectedrows = await db.ExecuteAsync(sqlQuery, new { id = request.Id });
            _logger.LogInformation("Удаление элемента - клиента с идентификатором id: {0}", request.Id);
            return affectedrows > 0;
            //await _repository.DeleteAsync(entity);
            //await _repository.CommitAsync(cancellationToken);
            //_logger.LogInformation("Удаление элемента - клиента с идентификатором id: {0}", request.Id);
            //return true;
        }
        _logger.LogError("Не удалось удалить элемент - клиент с идентификатором Id: {0}", request.Id);
        return false;
    }
}

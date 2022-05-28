namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик изменения элемента
/// </summary>
public class PatchClientCommandHandler : IRequestHandler<PatchClientCommand, bool>
{
    private readonly IClientRepository _repository;
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<PatchClientCommandHandler> _logger;
    /// <summary> </summary>
    public PatchClientCommandHandler(IClientRepository repository, IDbConnectionFactory connectionFactory, ILogger<PatchClientCommandHandler> logger)
    {
        _repository = repository;
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    /// <summary>
    /// Обработка изменения элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Успешность удаления</returns>
    public async Task<bool> Handle(PatchClientCommand request, CancellationToken cancellationToken)
    {
        if (!request.Patch.Operations.Any())
            throw new ArgumentNullException(nameof(request.Patch.Operations));

        using var db = _connectionFactory.CreateConnection();
        var patching = (await db.QueryAsync<Client>(@"
SELECT * FROM Clients 
WHERE Id = @id",
        new { request.Id })).FirstOrDefault();
        if (patching is { })
        {
            var patch = request.Patch;
            patch.ApplyTo(patching);
            var sqlQuery = @"
UPDATE Clients 
SET UserId = @UserId, LastName = @LastName, FirstName = @FirstName, Patronymic = @Patronymic, BirthDay = @BirthDay
WHERE Id = @Id";
            var affectedrows = await db.ExecuteAsync(sqlQuery, patching);
            _logger.LogInformation("Изменение клиента с идентификатором Id: {0}", patching.Id);
            return affectedrows > 0;
        }
        _logger.LogInformation("Не удалось изменить клиента с идентификатором Id: {0}", patching.Id);
        //var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        //if (item is { })
        //{
        //    var patch = request.Patch;
        //    patch.ApplyTo(item);
        //    await _repository.CommitAsync(cancellationToken);
        //    _logger.LogInformation("Изменение клиента с идентификатором Id: {0}", item.Id);
        //    return true;
        //}
        //_logger.LogError("Не удалось изменить клиента с идентификатором id: {0}", item?.Id);
        return false;
    }
}

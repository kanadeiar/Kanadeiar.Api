namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик команда добавления и обновления элемента
/// </summary>
public class AddUpdateClientCommandHandler : IRequestHandler<AddUpdateClientCommand, int>
{
    private readonly IClientRepository _repository;
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<AddUpdateClientCommandHandler> _logger;
    /// <summary> </summary>
    public AddUpdateClientCommandHandler(IClientRepository repository, IDbConnectionFactory connectionFactory, ILogger<AddUpdateClientCommandHandler> logger)
    {
        _repository = repository;
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    /// <summary>
    /// Команда добавления или обновления элемента
    /// </summary>
    /// <param name="request">запрос</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>Идентификатор элемента</returns>
    public async Task<int> Handle(AddUpdateClientCommand request, CancellationToken cancellationToken)
    {
        var config = new TypeAdapterConfig().ForType<ClientDto, Client>().Ignore(x => x.Id).Config;
        var item = request.Model.Adapt<Client>(config);
        if (request.Id == 0)
        {
            using var db = _connectionFactory.CreateConnection();
            var sqlQuery = @"
INSERT INTO Clients (UserId, LastName, FirstName, Patronymic, BirthDay) 
VALUES (@UserId, @LastName, @FirstName, @Patronymic, @BirthDay);
SELECT CAST(SCOPE_IDENTITY() as int)";
            int? itemId = await db.QueryFirstAsync<int>(sqlQuery, item);
            _logger.LogInformation("Добавление нового элемента - клиента с новым идентификатором id: {0}", itemId.Value);
            return itemId.Value;
            //await _repository.AddAsync(item, cancellationToken);
            //await _repository.CommitAsync(cancellationToken);
            //_logger.LogInformation("Добавление нового элемента - клиента с новым идентификатором id: {0}", item.Id);
            //return item.Id;
        }
        else
        {
            using var db = _connectionFactory.CreateConnection();
            var updating = (await db.QueryAsync<Client>(@"
SELECT * FROM Clients 
WHERE Id = @id",
            new { request.Id })).FirstOrDefault();
            if (updating is { })
            {
                item.Adapt(updating, config);
                updating.Id = request.Id;
                var sqlQuery = @"
UPDATE Clients 
SET UserId = @UserId, LastName = @LastName, FirstName = @FirstName, Patronymic = @Patronymic, BirthDay = @BirthDay
WHERE Id = @Id";
                var affectedrows = await db.ExecuteAsync(sqlQuery, updating);
                if (affectedrows > 0)
                {
                    return updating.Id;
                }
            }
        }
        //else if (await _repository.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        //{
        //    item.Adapt(entity, config);
        //    entity.Id = request.Id;
        //    await _repository.UpdateAsync(entity, cancellationToken);
        //    await _repository.CommitAsync(cancellationToken);
        //    _logger.LogInformation("Обновление элемента - клиента с идентификатором id: {0}", entity.Id);
        //    return entity.Id;
        //}
        _logger.LogError("Не удалось добавить или обновить сущность с идентификатором id: {0}", request.Id);
        return 0;
    }
}

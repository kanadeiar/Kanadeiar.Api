﻿namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик изменения элемента
/// </summary>
public class PatchClientHandler : IRequestHandler<PatchClient, bool>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<PatchClientHandler> _logger;
    /// <summary> </summary>
    public PatchClientHandler(IClientRepository repository, ILogger<PatchClientHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Обработка изменения элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Успешность удаления</returns>
    public async Task<bool> Handle(PatchClient request, CancellationToken cancellationToken)
    {
        if (!request.Patch.Operations.Any())
            throw new ArgumentNullException(nameof(request.Patch.Operations));
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (item is { })
        {
            var patch = request.Patch;
            patch.ApplyTo(item);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Изменение новости с идентификатором Id: {0}", item.Id);
            return true;
        }
        _logger.LogError("Не удалось изменить новость с идентификатором id: {0}", item?.Id);
        return false;
    }
}

using Microsoft.AspNetCore.JsonPatch;

namespace Rest1ClientApplication.Contracts.Commands;

/// <summary>
/// Изменить элемент
/// </summary>
public class PatchClient : IRequest<bool>
{
    /// <summary>
    /// Идентификатор изменяемого элемента
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Изменения элемента
    /// </summary>
    public JsonPatchDocument<Client> Patch { get; }

    /// <summary>
    /// Изменить элемент
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="patch">Изменения</param>
    public PatchClient(int id, JsonPatchDocument<Client> patch)
    {
        Id = id;
        Patch = patch;
    }
}

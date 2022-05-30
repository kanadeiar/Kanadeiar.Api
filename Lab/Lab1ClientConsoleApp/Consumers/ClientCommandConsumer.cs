namespace Lab1ClientConsoleApp.Consumers;

public class ClientCommandConsumer : IConsumer<AddClientCommand.IClientAdded>, IConsumer<UpdateClientCommand.IClientUpdated>, IConsumer<DeleteClientCommand.IClientDeleted>
{
    public async Task Consume(ConsumeContext<AddClientCommand.IClientAdded> context)
    {        
        await Task.Run(() =>
        {
            StaticClientData.ClientId = context.Message.Client.Id;
            StaticClientData.IsAdded = true;
            Console.WriteLine($"Получение сообщения от отправителя о создании нового клиента: Id: {context.Message.Client.Id} фамилия: {context.Message.Client.LastName} имя: {context.Message.Client.FirstName}");
        });
    }

    public async Task Consume(ConsumeContext<UpdateClientCommand.IClientUpdated> context)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"Получение сообщения от отправителя о обновлении клиента: Id: {context.Message.Id} фамилия: {context.Message.Client.LastName} имя: {context.Message.Client.FirstName}");
        });
    }

    public async Task Consume(ConsumeContext<DeleteClientCommand.IClientDeleted> context)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"Получение сообщения от отправителя о удалении клиента: Id: {context.Message.Id}");
        });
    }
}

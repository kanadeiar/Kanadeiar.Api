namespace RabbitMq1ClientConsoleApp.Requests;

public static class CreateCommand
{
    public static async Task SendRequestForInvoiceCreated(IPublishEndpoint endpoint, int key, string name)
    {
        await endpoint.Publish<IClientToCreate>(new
        {
            CutomerNumber = key,
            Model = new
            {
                Name = name,
            }
        });
    }
}

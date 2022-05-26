namespace RabbitMq1ClientConsoleApp.Requests;

public static class GetQuery
{
    public static async Task<IGetClientResult> SendQueryGetClient(IBus bus, int keyCount)
    {
        var client = bus.CreateRequestClient<IGetClient>();
        var response = await client.GetResponse<IGetClientResult>(new { Value = keyCount });
        return response.Message;
    }
}

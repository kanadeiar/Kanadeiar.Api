# gRPC API

[Назад](./../../README.md)

## Главное

Начальный шаг - создать gRpc сервис:

```sharp
dotnet new grpc
```

0. Основа приложения: сущности и интерфейсы - в Domain (ссылка на Kanadeiar.Core), бизнес-логика и общее - в Application (ссылки на Domain), База данных и специфическое - в Infrastructure (ссылки на Application), детальная реализация - в Api (ссылки на Infrastructure)


### Серверная часть

1. Создать "файл буфера протокола" и поместить его в папку "Protos" в Application.

Содержимое-образец "client.proto":

```sharp
syntax = "proto3";

import "google/protobuf/timestamp.proto";

option csharp_namespace = "gRpc1ClientApplication";

package client;

service ClientInfo {
  rpc GetById (IdRequest) returns (ClientDto);
}

message IdRequest {
  int Id = 1;
}

message ClientDto {
  int Id = 1;
  int UserId = 2;
  string LastName = 3;
  string FirstName = 4;
  string Patronymic = 5;
  google.protobuf.Timestamp BirthDay = 6;
  bytes RowVersion = 7;
}

syntax = "proto3";
```

2. Добавить файл протокола в серверную часть:

```sharp
<ItemGroup>
    <Protobuf Include="Protos\client.proto" GrpcServices="Server" />
</ItemGroup>
```
3. Создать сервис обработки запроса на основе протокола в проекте сервера:

```sharp
public class ClientService : ClientInfo.ClientInfoBase
{
    public override Task<ClientDto> GetById(IdRequest request, ServerCallContext context)
    {
        var client = new ClientDto { ... };
        return Task.FromResult(client);
    }
}
```

4. Проверить настройки серверного приложения для использования gRPC и включить сервис-обработчик:

```sharp
builder.Services.AddGrpc(); //сервисы
app.MapGrpcService<ClientGrpcService>(); //в конвейере
```

## Клиентская часть

1. В клиентской части "Добавить сервис" -> "gRPC" -> "Выбрать файл созданный выше"

2. Образец кода клиента для запроса ответа с сервера:

```sharp
var channel = GrpcChannel.ForAddress(new Uri("https://localhost:5001"));
var client = new ClientInfo.ClientInfoClient(channel);

while (true)
{
    try
    {
        var data = await client.GetByIdAsync(new GetByIdRequest { Id = 1 });
        ...
    }
    catch ...
}
```

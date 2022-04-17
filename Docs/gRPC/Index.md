# gRPC API

[Назад](./../../README.md)

## Главное

### Серверная часть

1. Создать "файл буфера протокола" и поместить его в папку "Protos" в проекте сервера.

Содержимое-образец "protobuf.proto":
```sharp
syntax = "proto3";

service PersonInform {
  rpc GetPersons (PersonRequest) returns (PersonsResponse);
}

message PersonRequest {
  int32 count = 1;
}

message Person {
	int32 id = 1;
	string surname = 2;
	string firstname = 3;
	int32 age = 4;
}

message PersonsResponse {
  repeated Person persons = 1;
}
```

2. Добавить пакет Grpc.AspNetCore

3. Добавить файл протокола в серверную часть:

```sharp
<ItemGroup>
    <Protobuf Include="Protos\protobuf.proto" GrpcServices="Server" />
</ItemGroup>
```
4. Создать сервис обработки запроса на основе протокола в проекте сервера:

```sharp
public class PersonInfoService : PersonInform.PersonInformBase
{
    public override async Task<PersonsResponse> GetPersons(PersonRequest request, ServerCallContext context)
    {
        var count = request.Count;
        return new PersonsResponse(new { ... });
    }
}
```

5. Настроить серверное приложение для использования gRPC и включить сервис-обработчик:

```sharp
builder.Services.AddGrpc(); //сервисы
app.MapGrpcService<PersonInfoService>(); //конвейер
```

## Клиентская часть

1. В клиентской части "Добавить сервис" -> "gRPC" -> "Выбрать файл созданный выше"

2. Образец кода клиента для запроса ответа с сервера:

```sharp
using var channel = GrpcChannel.ForAddress(new Uri("https://localhost:5001"));
var client = new PersonInform.PersonInformClient(channel);

while (true)
{
    try
    {
        var response = await client.GetPersonsAsync(new PersonRequest { Count = 10 });
        ...
    }
    catch ...
}
```

# Памятка по gRPC-сервису

## Основное по серверной части

1. Создать "файл буфера протокола" и поместить его в папку "Protos" в проекте сервера или общей библиотеке.

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
4. Создать сервис обработки запроса на основе протокола:

```sharp
public class PersonInfoService : PersonInform.PersonInformBase
{
    public override async Task<PersonsResponse> GetPersons(PersonRequest request, ServerCallContext context)
    {
        var count = request.Count;
        return new PersonsResponse(...);
    }
}
```

5. Настроить серверное приложение для использования gRPC и сервиса-обработкичка:

```sharp
builder.Services.AddGrpc();
app.MapGrpcService<PersonInfoService>();
```

## Основное по клиентской части

1. В клиентской части "Добавить сервис" -> "gRPC" -> "Выбрать файл созданный выше"

2. Образец кода клиента для получения ответа с сервера:

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

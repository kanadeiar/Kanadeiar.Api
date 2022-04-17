# gRPC API

[�����](./../../README.md)

## �������

### ��������� �����

1. ������� "���� ������ ���������" � ��������� ��� � ����� "Protos" � ������� �������.

����������-������� "protobuf.proto":
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

2. �������� ����� Grpc.AspNetCore

3. �������� ���� ��������� � ��������� �����:

```sharp
<ItemGroup>
    <Protobuf Include="Protos\protobuf.proto" GrpcServices="Server" />
</ItemGroup>
```
4. ������� ������ ��������� ������� �� ������ ��������� � ������� �������:

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

5. ��������� ��������� ���������� ��� ������������� gRPC � �������� ������-����������:

```sharp
builder.Services.AddGrpc(); //�������
app.MapGrpcService<PersonInfoService>(); //��������
```

## ���������� �����

1. � ���������� ����� "�������� ������" -> "gRPC" -> "������� ���� ��������� ����"

2. ������� ���� ������� ��� ������� ������ � �������:

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

# REST API

[�����](./../../README.md)

## �������

��������� ����:

```sharp
dotnet new web
```

1. � ������ ������ � ���� Program ���������������� ����������� �������:

```sharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
```

2. � ���� �� ����� ���������������� �������� ��������:

```sharp
app.MapControllers();
```

3. �������� ���������� �������� - API-���������� � ����� "Controllers":

```sharp
[ApiController]
[Route("value")]
public class ValueController : ControllerBase
{
}
```

4. ���������������� Swagger, Mapster, KarErrorHandler �� ����������.

5. ��������� ������ ����������:
```sharp
"launchUrl": "swagger",
"applicationUrl": "https://localhost:6001;http://localhost:6000",
```

## �����������:

�� �������� �������� Api ���������� � ����� cproj � ������ "PropertyGroup":
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

�� �������� �������������� ������������ � �������� ������ ������������ ������� � ������������

�������� ����������� ������������ ��� �������� ������� "*.xml" � ���������� � ���������� "**.xml", �������� �� � Swagger

��������� ������� � ������ �������:

```xml
services.AddCors(); //� ��������
app.UseCors(builder => builder.AllowAnyOrigin()); //� ��������� ����� �������� ����� �������������
```

### ������������

�������� ����� 
```sharp
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```
�������� ������������ � ������������:

```sharp
services.AddControllers().AddNewtonsoftJson();
```

### ������� �������

��������� �����:
```csharp
[HttpGet]
[SwaggerOperation(Summary = "�������� ��������", Description = "�������� ����� �������� - ����� �� ������")]
[SwaggerResponse(StatusCodes.Status200OK, "����� �� �������", Type = typeof(string))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "������ ������", Type = typeof(string))]
public string Value(string value)
{
    return $"Hello, {value}!";
}
```

���������� �����:
```csharp
HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:6001");
var response = await httpClient.GetAsync($"/value?value=Test");
var message = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
```

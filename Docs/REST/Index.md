# REST API

[�����](./../../README.md)

## �����������:

�� �������� �������� Api ���������� � ����� cproj � ������ "PropertyGroup":
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

�� �������� �������������� ������������ � �������� ������ ������������ ������� � ������������


## �������

��������� ����:

```sharp
dotnet new web
```

1. ������ ����������: �������� � ���������� - � Domain (������ �� Kanadeiar.Core), ������-������ � ����� - � Application (������ �� Kanadeiar.Api � Domain), ���� ������ � ������������� - � Infrastructure (������ �� Application), ��������� ���������� - � Api (������ �� Infrastructure)

1. � ������ ������ � ���� Program ���������������� ����������� �������:

```sharp
builder.Services.AddControllers();
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
4. ��������� ������� � ������ �������:

```xml
services.AddCors(); //� ��������
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //� ��������� ����� �������� ����� �������������
```

5. ��������� ������ ���������� � ����� launchSettings.json:

```sharp
"launchUrl": "swagger",
"applicationUrl": "https://localhost:6001;http://localhost:6000",
```

6. �������� ����������� ������������.

��� �������� ������� "*.xml" � ���������� � �������� "*.sample.xml", �������� �� � Swagger

7. �c���������� ���������� Kanadeiar.Api.

[������������ �� ����������](./../../Kanadeiar.Api/README.md)

### ���� �����:

�������������� ��������������� �� https:

```csharp
app.UseHttpsRedirection(); //����� app.UseRouting()
```
����������� ��������� � https:

```cshapr
app.UseHsts();
```

### ���� ������ EF

[��������� �� ���� ������](./Database.md).

### �����������

���������� ��������������� ����� ������-������� � ����� ������

[��������� �� ������������](./Repositories.md).

### �������� MediatR

��� ������������� ������� CQRS � �������. ��� ������� � Kanadeiar.Api.

[���������� �� ���������](./MediatR.md).

### ��������� FluentValidation

��� �������� ������������ �������� ������

[���������� �� ���������](./FluentValidation.md).

### ������������

�������� ����� 
```sharp
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```
�������� ������������ � ������������:

```sharp
services.AddControllers().AddNewtonsoftJson();
```




### ������� ������� � api-����������� ����������

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

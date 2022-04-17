# REST API

[�����](./../../README.md)

## �������

��������� ����:

1. � ������ ������ � ���� Program ���������������� ����������� �������:

```sharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
```

2. � ���� �� ����� ���������������� �������� ��������:

```sharp
app.MapControllers();
```

3. �������� ���������� �������� - ����������:

```sharp
[ApiController]
[Route("api/test")]
public class ValuesController : ControllerBase
{
}
```

## �����������:

�� �������� �������� Api ���������� � ����� cproj:
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

�� �������� �������������� ������������ � �������� ������ ������������ ������� � ������������

�������� ����������� ������������ ��� �������� ������� "*.xml" � ���������� � ���������� "**.xml", �������� �� � Swagger

��������� ������� � ������ �������:

```xml
services.AddCors();
app.UseCors(builder => builder.AllowAnyOrigin());
```

### ������������

�������� ����� 
```charp
�����: Microsoft.AspNetCore.Mvc.NewtonsoftJson
```
�������� ������������ � ������������:

```sharp
services.AddControllers().AddNewtonsoftJson();
```

### ������� �������

��������� �����:
```csharp
[HttpGet("{count}")]
[SwaggerOperation(Summary = "�������� �����������", Description = "�������� ����������� � ������ ����������")]
[SwaggerResponse(StatusCodes.Status200OK, "����������", Type = typeof(IEnumerable<Person>))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "������ ������", Type = typeof(string))]
[SwaggerResponse(StatusCodes.Status404NotFound, "�� ������")]
public async Task<IActionResult> GetAllValues(int count)
{
    if (count == 0)
        return NotFound();
    _logger.LogInformation("��������� �������� ������");
    var persons = await _personService.GetPersons(count);
    return Ok(persons);
}
```

���������� �����:
```csharp
HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:6001");
var response = await httpClient.GetAsync($"/api/test/{10}");
var persons = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IEnumerable<Person>>();
```

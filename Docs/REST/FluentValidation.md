# ��������� FluentValidation

[�����](./Index.md)

�������� ����� � Infrastructure
```sharp
dotnet add package FluentValidation.AspNetCore
```

� ���� Infrastructure ������� ������� ��������� ������:

```sharp
public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    public ClientDtoValidator()
    {
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).WithMessage("������ ������� ������ ���� ������ 3 ��������").WithErrorCode("400");
        RuleFor(x => x.LastName).MaximumLength(100).WithMessage("������ ������� ������ ���� ������ 100 ��������").WithErrorCode("400");
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).WithMessage("������ ����� ������ ���� ������ 3 ��������").WithErrorCode("400");
        RuleFor(x => x.FirstName).MaximumLength(100).WithMessage("������ ����� ������ ���� ������ 100 ��������").WithErrorCode("400");
        RuleFor(x => x.Patronymic).NotEmpty().MinimumLength(3).WithMessage("������ �������� ������ ���� ������ 3 ��������").WithErrorCode("400");
        RuleFor(x => x.Patronymic).MaximumLength(100).WithMessage("������ �������� ������ ���� ������ 100 ��������").WithErrorCode("400");
        RuleFor(x => x.BirthDay).LessThan(DateTime.Today).WithMessage("���� �������� �� ����� ���� � �������").WithErrorCode("400");
        RuleFor(x => x.BirthDay).GreaterThan(DateTime.Today.AddYears(-200)).WithMessage("������� �� ����� ���� ������ 200 ���").WithErrorCode("400");
    }
}
```
� ���� Infrastructure ������� ����������� ��������� � ��������:
```sharp
public static IServiceCollection MyAddFluentValidation(this IServiceCollection services)
{
    services.AddFluentValidation();
    services.AddTransient<IValidator<ClientDto>, ClientDtoValidator>();

    return services;
}
```

������������ � ����������:
```sharp
builder.Services.MyAddFluentValidation();
```

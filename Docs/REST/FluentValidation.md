# Валидация FluentValidation

[Назад](./Index.md)

Добавить пакет в Infrastructure
```sharp
dotnet add package FluentValidation.AspNetCore
```

В слое Infrastructure создать правила валидации дтошки:

```sharp
public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    public ClientDtoValidator()
    {
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).WithMessage("Длинна фамилии должна быть больше 3 символов").WithErrorCode("400");
        RuleFor(x => x.LastName).MaximumLength(100).WithMessage("Длинна фамилии должна быть меньше 100 символов").WithErrorCode("400");
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).WithMessage("Длинна имени должна быть больше 3 символов").WithErrorCode("400");
        RuleFor(x => x.FirstName).MaximumLength(100).WithMessage("Длинна имени должна быть меньше 100 символов").WithErrorCode("400");
        RuleFor(x => x.Patronymic).NotEmpty().MinimumLength(3).WithMessage("Длинна отчества должна быть больше 3 символов").WithErrorCode("400");
        RuleFor(x => x.Patronymic).MaximumLength(100).WithMessage("Длинна отчества должна быть меньше 100 символов").WithErrorCode("400");
        RuleFor(x => x.BirthDay).LessThan(DateTime.Today).WithMessage("Дата рождения не может быть в будущем").WithErrorCode("400");
        RuleFor(x => x.BirthDay).GreaterThan(DateTime.Today.AddYears(-200)).WithMessage("Возраст не может быть больше 200 лет").WithErrorCode("400");
    }
}
```
В слое Infrastructure создать регистратор валидации в сервисах:
```sharp
public static IServiceCollection MyAddFluentValidation(this IServiceCollection services)
{
    services.AddFluentValidation();
    services.AddTransient<IValidator<ClientDto>, ClientDtoValidator>();

    return services;
}
```

Использовать в приложении:
```sharp
builder.Services.MyAddFluentValidation();
```

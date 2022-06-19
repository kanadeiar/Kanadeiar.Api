namespace MT1GatewayInfra.Validators;

/// <summary>
/// настройка валидации
/// </summary>
public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    /// <summary>
    /// Настройка валидации
    /// </summary>
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

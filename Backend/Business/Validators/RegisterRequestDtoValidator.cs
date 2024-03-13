using Entities.DTOs;
using FluentValidation;

namespace Business.Validators;
internal class RegisterRequestDtoValidator : AbstractValidator<CreateUserDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty().WithMessage("İsim boş olamaz!");
        RuleFor(u => u.FirstName).NotNull().WithMessage("İsim boş olamaz!");
        RuleFor(u => u.FirstName).MinimumLength(3).WithMessage("İsim en az 3 karakter olmalıdır!");

        RuleFor(u => u.LastName).NotEmpty().WithMessage("Soyisim boş olamaz!");
        RuleFor(u => u.LastName).NotNull().WithMessage("Soyisim boş olamaz!");
        RuleFor(u => u.LastName).MinimumLength(3).WithMessage("Soyisim en az 3 karakter olmalıdır!");
    }
}

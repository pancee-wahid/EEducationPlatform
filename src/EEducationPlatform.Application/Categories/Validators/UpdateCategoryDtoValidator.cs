using EEducationPlatform.Categories.Dtos;
using FluentValidation;

namespace EEducationPlatform.Categories.Validators;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(3);
    }
}
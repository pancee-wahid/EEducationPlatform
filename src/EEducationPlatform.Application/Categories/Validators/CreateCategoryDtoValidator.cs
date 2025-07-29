using System;
using System.Linq;
using EEducationPlatform.Categories.Dtos;
using EEducationPlatform.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.Categories.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator(IStringLocalizer<EEducationPlatformResource> localizer)
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(StringLength.Name);

        RuleFor(e => e.Code)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(StringLength.Code)
            .Must(c => !c.Any(Char.IsWhiteSpace))
            .WithMessage(localizer["Validation:MustNotContainWhiteSpace"]);

        RuleFor(e => e.Description)
            .MaximumLength(StringLength.Description)
            .When(e => !e.Description.IsNullOrEmpty());
    }
}
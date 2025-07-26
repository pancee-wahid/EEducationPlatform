using System;
using System.Linq;
using EEducationPlatform.Categories.Dtos;
using EEducationPlatform.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EEducationPlatform.Categories.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator(IStringLocalizer<EEducationPlatformResource> localizer)
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(e => e.Code)
            .NotEmpty()
            .MinimumLength(3)
            .Must(c => !c.Any(Char.IsWhiteSpace))
            .WithMessage(localizer["Validation:MustNotContainWhiteSpace"]);
    }
}
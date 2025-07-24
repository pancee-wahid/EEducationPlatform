using EEducationPlatform.Categories.Dtos;
using FluentValidation;

namespace EEducationPlatform.Categories.Validators;

public class GetCategoryQueryDtoValidator : AbstractValidator<GetCategoryQueryDto>
{
    public GetCategoryQueryDtoValidator()
    {
        RuleFor(e => e.MaxDepth)
            .InclusiveBetween(0, 5);
    }
}
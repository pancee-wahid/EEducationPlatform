using System;
using System.Linq;
using EEducationPlatform.Courses.Dtos;
using EEducationPlatform.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;
using static EEducationPlatform.EEducationPlatformConstants.Validations;
namespace EEducationPlatform.Courses.Validators;

public class CourseRequestDtoValidator : AbstractValidator<CourseRequestDto>
{
    public CourseRequestDtoValidator(IStringLocalizer<EEducationPlatformResource> localizer)
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(StringLength.Name);
        
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(StringLength.Code)
            .Must(c => !c.Any(Char.IsWhiteSpace))
            .WithMessage(localizer["Validation:MustNotContainWhiteSpace"]);
        
        RuleFor(e => e.Description)
            .MaximumLength(StringLength.Description)
            .When(e => !e.Description.IsNullOrEmpty());

        RuleFor(e => e.IsPaid).NotNull();
        
        RuleFor(e => e.SubscriptionFees)
            .NotEmpty()
            .When(e => e.IsPaid);
        
        RuleFor(e => e.NeedsEnrollmentApproval).NotNull();
    }
    
}
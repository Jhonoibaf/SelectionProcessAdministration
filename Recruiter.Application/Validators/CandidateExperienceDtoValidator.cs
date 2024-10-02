using FluentValidation;
using Recruiters.Application.DTOs;

namespace Recruiters.Application.Validators
{
    public class CandidateExperienceDtoValidator : AbstractValidator<CandidateExperienceDto>
    {
        public CandidateExperienceDtoValidator()
        {
            RuleFor(c => c.Company)
                .Length(2, 100).WithMessage("Company must be between 2 and 100 characters.");

            RuleFor(c => c.Job)
                .NotEmpty().WithMessage("Job title is required.")
                .Length(2, 50).WithMessage("Job title must be between 2 and 50 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(4000).WithMessage("Description must not exceed 4000 characters.");

            RuleFor(c => c.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive value.");

            RuleFor(c => c.BeginDate)
                .NotEmpty().WithMessage("Begin date is required.")
                 .LessThan(DateTime.Today).WithMessage("End date cannot be in the future.");

            RuleFor(c => c.EndDate)
                .GreaterThan(c => c.BeginDate).WithMessage("End date must be after Begin date.")
                .LessThan(DateTime.Today).WithMessage("End date cannot be in the future.");
        }
    }
}

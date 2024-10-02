using FluentValidation;
using Recruiters.Application.DTOs;

namespace Recruiters.Application.Validators
{
    public class CandidateDtoValidator : AbstractValidator<CandidateDto>
    {
        public CandidateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters."); 
            
            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 150).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.Birthdate)
                .NotEmpty().WithMessage("Birthdate is required.")
                .LessThan(DateTime.Today).WithMessage("Birthdate cannot be in the future.");
        }
    }
}

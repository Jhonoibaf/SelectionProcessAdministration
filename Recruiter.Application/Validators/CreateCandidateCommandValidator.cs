using FluentValidation;
using Recruiters.Application.CandidatesAdministration.Commands;

namespace Recruiters.Application.Validators
{
    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand.Command>
    {
        public CreateCandidateCommandValidator()
        {
            RuleFor(c => c.Candidate.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(c => c.Candidate.Surname) 
                .NotEmpty().WithMessage("Surname is required.")
                .Length(2, 150).WithMessage("Surname must be between 2 and 150 characters.");

            RuleFor(c => c.Candidate.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.Candidate.Birthdate)
                .NotEmpty().WithMessage("Birthdate is required.")
                .LessThan(DateTime.Today).WithMessage("Birthdate cannot be in the future.");
        }
    }
}

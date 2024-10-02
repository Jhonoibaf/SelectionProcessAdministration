using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Application.DTOs;
using Recruiters.Application.Validators;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;


namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class UpdateCandidateCommand
    {
        public record Command(CandidateDto candidate) : IRequest<Candidate>;

        public class Handler : IRequestHandler<Command, Candidate>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            private readonly CandidateDtoValidator _validator;

            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
                _validator = new CandidateDtoValidator();
            }

            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var validationResult = _validator.Validate(request.candidate);
                    if (!validationResult.IsValid)
                    {
                        List<string> errorMessages = [];

                        foreach (ValidationFailure error in validationResult.Errors)
                        {
                            errorMessages.Add(error.ErrorMessage);
                        }
                        throw new Exception(string.Join(",", errorMessages));
                    }

                    var candidate = _mapper.Map<Candidate>(request.candidate);
                    candidate.Validate();

                    var existingCandidate = await _dbcontext.Candidates
                                    .Where(c => c.IdCandidate == request.candidate.IdCandidate)
                                    .FirstOrDefaultAsync();

                    if (existingCandidate == null) throw new Exception("Candidate not found");

                    var candidateModel = _mapper.Map(request.candidate, existingCandidate);
                    _dbcontext.Update(candidateModel);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidate = _mapper.Map<Candidate>(candidateModel);
                    return candidate;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }
    }

    public record Response(Candidate Candidate);
}


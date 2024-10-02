using AutoMapper;
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
                var validationResult = _validator.Validate(request.candidate);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception(errorMessages);
                }

                var candidate = _mapper.Map<Candidate>(request.candidate);
                candidate.Validate();

                var existingCandidate = await _dbcontext.Candidates
                    .FirstOrDefaultAsync(c => c.IdCandidate == request.candidate.IdCandidate, cancellationToken);

                if (existingCandidate == null)
                {
                    throw new Exception("Candidate not found");
                }

                _mapper.Map(request.candidate, existingCandidate);
                _dbcontext.Update(existingCandidate);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Candidate>(existingCandidate);
            }
        }
        public record Response(Candidate Candidate);
    }

}


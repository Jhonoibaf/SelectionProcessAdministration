using AutoMapper;
using MediatR;
using Recruiters.Application.DTOs;
using Recruiters.Application.Validators;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;
using Recruiters.Infraestructure.Models;


namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class CreateCandidateCommand
    {
        public record Command(CandidateDto Candidate) : IRequest<Candidate>;
        public class Handler : IRequestHandler<Command, Candidate>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            private readonly CandidateDtoValidator _validator;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
                _validator = new CandidateDtoValidator();
            }

            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                    var validationResult = _validator.Validate(request.Candidate);
                    if (!validationResult.IsValid)
                    {
                        var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                        throw new Exception(errorMessages);
                    }

                    var candidate = _mapper.Map<Candidate>(request.Candidate);
                    candidate.Validate();

                    var candidateToCreate = _mapper.Map<CandidateModel>(request.Candidate);
                    if (candidateToCreate == null) throw new Exception("Candidate not be create");

                    candidateToCreate.InsertDate = DateTime.Now;
                    _dbcontext.Add(candidateToCreate);
                    await _dbcontext.SaveChangesAsync(cancellationToken);

                    return _mapper.Map<Candidate>(candidateToCreate);
            }
        }

        public record Response(Candidate Candidate);


    }
}

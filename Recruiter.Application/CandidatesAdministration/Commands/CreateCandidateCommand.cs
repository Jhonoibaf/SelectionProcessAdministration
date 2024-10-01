using AutoMapper;
using MediatR;
using Recruiters.Application.DTOs;
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
            public Handler(ApplicationDbContext dbcontext , IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
            }

            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var candidateModel = new CandidateModel();
                    var candidateToCreate = _mapper.Map(request.Candidate, candidateModel);
                    if (candidateToCreate == null) throw new Exception("Candidate not be create");

                    _dbcontext.Add(candidateToCreate);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidateCreated = _mapper.Map<Candidate>(candidateToCreate);
                    return candidateCreated;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }

        public record Response(Candidate Candidate);
    }
}

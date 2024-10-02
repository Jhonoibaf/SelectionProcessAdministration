using AutoMapper;
using MediatR;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class DeleteCadidateCommand
    {
        public record Command(int Id) : IRequest<Candidate>;
        public class Handler : IRequestHandler<Command, Candidate>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
            }
            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var candidateModel = await _dbcontext.Candidates.FindAsync(request.Id);
                    if (candidateModel == null)
                    {
                        throw new Exception("Candidate not found");
                    }

                    _dbcontext.Candidates.Remove(candidateModel);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidate = _mapper.Map<Candidate>(candidateModel);
                    return candidate;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting candidate: {ex.Message}", ex);
                }
            }
        }
        public record Response(Candidate Candidate);
    }
}

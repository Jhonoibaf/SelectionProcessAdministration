using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.CandidatesAdministration.Queries
{
    public class GetCandidateByIdQuery
    {
        public record Query(int Id) : IRequest<Candidate>;

        public class Handler : IRequestHandler<Query, Candidate>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }
            
            public async Task<Candidate> Handle(Query request, CancellationToken cancellationToken)
            {
                var candidate = await _dbcontext.Candidates.FindAsync(request.Id);
                return candidate == null ? throw new Exception("Candidate not found") : candidate;
            }
        }

        public record Response(Candidate Candidate);

    }
}

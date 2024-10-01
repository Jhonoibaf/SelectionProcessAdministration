using AutoMapper;
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
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext dbcontext , IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
            }
            
            public async Task<Candidate> Handle(Query request, CancellationToken cancellationToken)
            {
                var candidateModel = await _dbcontext.Candidates
                            .Include(c => c.CandidateExperiences)
                            .FirstOrDefaultAsync(c => c.IdCandidate == request.Id);

                var candidate = _mapper.Map<Candidate>(candidateModel);
                return candidate == null ? throw new Exception("Candidate not found") : candidate;
            }
        }

        public record Response(Candidate Candidate);

    }
}

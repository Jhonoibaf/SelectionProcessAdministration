using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.CandidatesAdministration.Queries
{
    public class GetAllCandidatesQuery
    {
        public record Query() : IRequest<List<Candidate>>;

        public class Handler : IRequestHandler<Query, List<Candidate>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
            }

            public async Task<List<Candidate>>Handle(Query request, CancellationToken cancellationToken)
            {
                var candidatesModel = await _dbcontext.Candidates.Include(c => c.CandidateExperiences).ToListAsync();
                var candidates = _mapper.Map<List<Candidate>>(candidatesModel);
                return candidates ?? throw new Exception("Candidates not found");
            }
        }

        public record Response(Candidate Candidate);

    }
}

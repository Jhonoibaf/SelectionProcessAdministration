using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruiters.Application.CandidatesAdministration.Queries
{
    public class GetAllCandidatesQuery
    {
        public record Query() : IRequest<List<Candidate>>;

        public class Handler : IRequestHandler<Query, List<Candidate>>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<List<Candidate>>Handle(Query request, CancellationToken cancellationToken)
            {
                var candidates = await _dbcontext.Candidates.Include(c => c.CandidateExperiences).ToListAsync();

                return candidates == null ? throw new Exception("Candidate not found") : candidates;
            }
        }

        public record Response(Candidate Candidate);

    }
}

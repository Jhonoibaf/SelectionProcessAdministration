using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.ExperiencesAdministration.Queries
{
    public class GetCandidateExperienceByIdQuery
    {
        // Cambiar el tipo de retorno del IRequest a CandidateExperience
        public record Query(int Id) : IRequest<CandidateExperience>;

        public class Handler : IRequestHandler<Query, CandidateExperience>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
            }

            public async Task<CandidateExperience> Handle(Query request, CancellationToken cancellationToken)
            {
                var candidateExperienceModel = await _dbcontext.CandidateExperiences
                                         .Include(ce => ce.Candidate)
                                         .FirstOrDefaultAsync(ce => ce.IdCandidateExperience == request.Id);

                var candidateExperience = _mapper.Map<CandidateExperience>(candidateExperienceModel);
                return candidateExperience == null ? throw new Exception("Candidate not found") : candidateExperience;
                throw new NotImplementedException();
            }
        }

        public record Response(CandidateExperience CandidateExperience);
    }
}

using AutoMapper;
using MediatR;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.ExperiencesAdministration.Commands
{
    public class DeleteCandidateExperienceCommand
    {
        public record Command(int Id) : IRequest<CandidateExperience>;
        public class Handler : IRequestHandler<Command, CandidateExperience>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _dbcontext = dbcontext;
                _mapper = mapper;
            }
            public async Task<CandidateExperience> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var candidateExperienceModel = await _dbcontext.CandidateExperiences.FindAsync(request.Id);

                    if (candidateExperienceModel == null)
                    {
                        throw new Exception("Candidate not found");
                    }

                    _dbcontext.Remove(candidateExperienceModel);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidateExperience = _mapper.Map<CandidateExperience>(candidateExperienceModel);
                    return candidateExperience;
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

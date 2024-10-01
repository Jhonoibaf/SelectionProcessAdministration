using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Application.DTOs;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.ExperiencesAdministration.Commands
{
    public class UpdateCandidateExperienceCommand
    {

        public record Command(CandidateExperienceDto candidate) : IRequest<CandidateExperience>;

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
                    var existingCandidateExperience = await _dbcontext.CandidateExperiences
                    .FirstOrDefaultAsync(ce => ce.IdCandidateExperience == request.candidate.IdCandidateExperience);

                    if (existingCandidateExperience == null)
                        throw new Exception("Candidate experience not found");

                    var candidateExperienceModel = _mapper.Map(request.candidate, existingCandidateExperience);

                    _dbcontext.Update(candidateExperienceModel);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidateExperience = _mapper.Map<CandidateExperience>(candidateExperienceModel);

                    return candidateExperience;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }
    }
}

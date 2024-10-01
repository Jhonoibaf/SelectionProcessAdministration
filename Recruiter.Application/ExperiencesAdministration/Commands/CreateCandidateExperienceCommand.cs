using AutoMapper;
using MediatR;
using Recruiters.Application.DTOs;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;
using Recruiters.Infraestructure.Models;

namespace Recruiters.Application.ExperiencesAdministration.Commands
{
    public class CreateCandidateExperienceCommand
    {
        public record Command(CandidateExperienceDto CandidateExperienceDto) : IRequest<CandidateExperience>;
        public class Handler : IRequestHandler<Command, CandidateExperience>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
            }

            public async Task<CandidateExperience> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var candidateEperienceModel = new CandidateExperienceModel();
                    var candidateExperienceToCreate = _mapper.Map(request.CandidateExperienceDto, candidateEperienceModel);
                    if (candidateExperienceToCreate == null) throw new Exception("Candidate not be create");

                    _dbcontext.Add(candidateExperienceToCreate);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidateExperienceCreated = _mapper.Map<CandidateExperience>(candidateExperienceToCreate);
                    return candidateExperienceCreated;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }

        public record Response(CandidateExperience CandidateExperience);
    }
}

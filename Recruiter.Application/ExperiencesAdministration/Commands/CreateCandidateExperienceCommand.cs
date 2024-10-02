using AutoMapper;
using MediatR;
using Recruiters.Application.DTOs;
using Recruiters.Application.Validators;
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
            private readonly CandidateExperienceDtoValidator _validator;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
                _validator = new CandidateExperienceDtoValidator();
            }

            public async Task<CandidateExperience> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request.CandidateExperienceDto);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new Exception(errorMessages);
                }

                var candidateExperienceToCreate = _mapper.Map<CandidateExperienceModel>(request.CandidateExperienceDto);
                if (candidateExperienceToCreate == null) throw new Exception("Candidate not be create");

                candidateExperienceToCreate.InsertDate = DateTime.Now;
                _dbcontext.Add(candidateExperienceToCreate);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<CandidateExperience>(candidateExperienceToCreate); 
            }
        }
        public record Response(CandidateExperience CandidateExperience);
    }
}

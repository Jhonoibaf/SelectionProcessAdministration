using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Application.DTOs;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;


namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class UpdateCandidateCommand
    {
        public record Command(CandidateDto candidate) : IRequest<Candidate>;

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
                    var existingCandidate = await _dbcontext.Candidates
                                    .Where(c => c.IdCandidate == request.candidate.IdCandidate)
                                    .FirstOrDefaultAsync();

                    if (existingCandidate == null) throw new Exception("Candidate not found");

                    var candidateModel = _mapper.Map(request.candidate, existingCandidate);
                    _dbcontext.Update(candidateModel);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidate = _mapper.Map<Candidate>(candidateModel);
                    return candidate;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }
    }

    public record Response(Candidate Candidate);
}


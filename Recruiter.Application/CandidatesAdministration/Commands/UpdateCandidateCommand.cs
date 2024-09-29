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
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var insertDate = await _dbcontext.Candidates
                                            .Where(c => c.IdCandidate == request.candidate.IdCandidate)
                                            .Select(c => c.InsertDate).FirstOrDefaultAsync();

                    var candidate = new Candidate
                    {
                        IdCandidate = request.candidate.IdCandidate,
                        Name = request.candidate.Name,
                        Surname = request.candidate.Surname,
                        Birthdate = request.candidate.Birthdate,
                        Email = request.candidate.Email,
                        InsertDate = insertDate,
                        ModifyDate = DateTime.Now
                    };
                    _dbcontext.Update(candidate);
                    await _dbcontext.SaveChangesAsync();
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


using MediatR;
using Recruiters.Application.DTOs;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class CreateCandidateCommand
    {
        public record Command(CandidateDto Candidate) : IRequest<Candidate>;
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
                    var candidate = new Candidate
                    {
                        IdCandidate = request.Candidate.IdCandidate,
                        Name = request.Candidate.Name,
                        Surname = request.Candidate.Surname,
                        Birthdate = request.Candidate.Birthdate,
                        Email = request.Candidate.Email,
                        InsertDate = DateTime.Now,
                        ModifyDate = DateTime.Now
                    };

                    if (candidate == null)
                    {
                        throw new Exception("Candidate not be create");
                    }

                    _dbcontext.Add(candidate);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    return candidate;


                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }

        public record Response(Candidate Candidate);
    }
}

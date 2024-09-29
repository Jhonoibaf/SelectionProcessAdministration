using MediatR;
using Microsoft.EntityFrameworkCore;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class CreateCandidateCommand
    {
        public record Command(Candidate Candidate) : IRequest<Candidate>;
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
                    var candidate = request.Candidate;

                    if (candidate != null)
                    {
                        _dbcontext.Add(candidate);
                        await _dbcontext.SaveChangesAsync();
                        return candidate;
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
                throw new Exception("Candidate not be create");
            }
        }

        public record Response(Candidate Candidate);
    }
}

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
    public class UpdateCandidateCommand
    {
        public record Command(Candidate candidate) : IRequest<Candidate>;

        public class Handler : IRequestHandler<Command, Candidate>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                var candidate = request.candidate;
                try
                {
                    _dbcontext.Update(candidate);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
                throw new Exception("Candidate not be create");
            }
        }
    }

    public record Response(Candidate Candidate);
}


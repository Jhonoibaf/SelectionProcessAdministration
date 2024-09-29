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
    public class DeleteCadidateCommand
    {
        public record Command(int Id) : IRequest<Candidate>;
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
                    var candidate = await _dbcontext.Candidates.FindAsync(request.Id);
                    if (candidate == null)
                    {
                        throw new Exception("Candidate not found");
                    }

                    _dbcontext.Candidates.Remove(candidate);
                    await _dbcontext.SaveChangesAsync(cancellationToken);

                    return candidate;
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

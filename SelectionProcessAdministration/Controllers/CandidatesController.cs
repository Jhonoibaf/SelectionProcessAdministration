using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruiters.Application.CandidatesAdministration.Commands;
using Recruiters.Application.CandidatesAdministration.Queries;
using Recruiters.Application.DTOs;
using Recruiters.Infraestructure.Data;

namespace SelectionProcessAdministration.Controllers
{
    public class CandidatesController(ApplicationDbContext context, IMediator mediator) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMediator _mediator = mediator;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Candidates.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery.Query(id));
            return View(candidate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCandidate(CandidateDto candidate)
        {
            var candidateCreated = await _mediator.Send(new CreateCandidateCommand.Command(candidate));
            return candidateCreated != null ? RedirectToAction(nameof(Index)) : View(candidateCreated);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery.Query(id));
            return View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidate(CandidateDto candidate)
        {
            var candidateUpdated = await _mediator.Send(new UpdateCandidateCommand.Command(candidate));
            return candidateUpdated != null ? RedirectToAction(nameof(Index)) : View(candidateUpdated);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery.Query(id));
            return View(candidate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteCadidateCommand.Command(id));
            return RedirectToAction(nameof(Index));
        }
    }
}

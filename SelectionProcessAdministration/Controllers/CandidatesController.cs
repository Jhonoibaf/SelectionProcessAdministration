using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruiters.Application.CandidatesAdministration.Commands;
using Recruiters.Application.CandidatesAdministration.Queries;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;
using SelectionProcessAdministration.Models.ViewModels;

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
        public async Task<IActionResult> Create(CanditateViewModel candidateViewModel)
        {
            var candidate = new Candidate
            {
                IdCandidate = candidateViewModel.IdCandidate,
                Name = candidateViewModel.Name,
                Surname = candidateViewModel.Surname,
                Birthdate = candidateViewModel.Birthdate,
                Email = candidateViewModel.Email,
                InsertDate = DateTime.Now, 
                ModifyDate = DateTime.Now 
            };

            var candidateCreated = await _mediator.Send(new CreateCandidateCommand.Command(candidate));
            return candidateCreated != null ? RedirectToAction(nameof(Index)): View(candidateCreated);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery.Query(id));
            return View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromBody] Candidate candidate)
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
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.IdCandidate == id);
        }
    }
}

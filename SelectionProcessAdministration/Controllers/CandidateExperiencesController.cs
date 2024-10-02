using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruiters.Application.CandidatesAdministration.Commands;
using Recruiters.Application.DTOs;
using Recruiters.Application.ExperiencesAdministration.Commands;
using Recruiters.Application.ExperiencesAdministration.Queries;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;

namespace SelectionProcessAdministration.Controllers
{
    public class CandidateExperiencesController(ApplicationDbContext context, IMediator mediator) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMediator _mediator = mediator;

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CandidateExperiences.Include(c => c.Candidate);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperiences
                .Include(c => c.Candidate)
                .FirstOrDefaultAsync(m => m.IdCandidateExperience == id);
            if (candidateExperience == null)
            {
                return NotFound();
            }

            return View(candidateExperience);
        }

        public IActionResult Create(int candidateId)
        {
            ViewData["IdCandidate"] = candidateId;
            var model = new CandidateExperience
            {
                IdCandidate = candidateId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateExperienceDto candidateExperienceDto)
        {
            var candidateExperienceCreated = await _mediator.Send(new CreateCandidateExperienceCommand.Command(candidateExperienceDto));
            return candidateExperienceCreated != null ? RedirectToAction("Index", "Candidates") : View(candidateExperienceCreated);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var candidateExperience = await _mediator.Send(new GetCandidateExperienceByIdQuery.Query(id));
            return View(candidateExperience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidateExperience(int id, CandidateExperienceDto candidateExperience)
        {
            var candidateExperienceUpdated = await _mediator.Send(new UpdateCandidateExperienceCommand.Command(candidateExperience));
            return candidateExperienceUpdated != null ? RedirectToAction("Index", "Candidates") : View(candidateExperienceUpdated);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var candidateExperience = await _mediator.Send(new GetCandidateExperienceByIdQuery.Query(id));
            return View(candidateExperience);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidateExperienceDeleted = await _mediator.Send(new DeleteCandidateExperienceCommand.Command(id));
            return candidateExperienceDeleted != null ? RedirectToAction("Index", "Candidates") : View(candidateExperienceDeleted);
        }

        private bool CandidateExperienceExists(int id)
        {
            return _context.CandidateExperiences.Any(e => e.IdCandidateExperience == id);
        }
    }
}

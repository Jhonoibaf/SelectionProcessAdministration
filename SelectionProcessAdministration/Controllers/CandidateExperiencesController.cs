using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Create()
        {
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidateExperience,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate,IdCandidate")] CandidateExperience candidateExperience)
        {
            _context.Add(candidateExperience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Email", candidateExperience.IdCandidate);
            //return View(candidateExperience);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateExperience = await _context.CandidateExperiences.FindAsync(id);
            if (candidateExperience == null)
            {
                return NotFound();
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Email", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidateExperience,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate,IdCandidate")] CandidateExperience candidateExperience)
        {
            if (id != candidateExperience.IdCandidateExperience)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExperienceExists(candidateExperience.IdCandidateExperience))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Email", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidateExperience = await _context.CandidateExperiences.FindAsync(id);
            if (candidateExperience != null)
            {
                _context.CandidateExperiences.Remove(candidateExperience);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExperienceExists(int id)
        {
            return _context.CandidateExperiences.Any(e => e.IdCandidateExperience == id);
        }
    }
}

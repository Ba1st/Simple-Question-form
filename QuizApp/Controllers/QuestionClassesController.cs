using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuestionClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionClass.ToListAsync());
        }
        
        // GET: QuestionClasses/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        
        // POST: QuestionClasses/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.QuestionClass.Where( j => j.Question.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: QuestionClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionClass = await _context.QuestionClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionClass == null)
            {
                return NotFound();
            }

            return View(questionClass);
        }

        // GET: QuestionClasses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] QuestionClass questionClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionClass);
        }

        // GET: QuestionClasses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionClass = await _context.QuestionClass.FindAsync(id);
            if (questionClass == null)
            {
                return NotFound();
            }
            return View(questionClass);
        }

        // POST: QuestionClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] QuestionClass questionClass)
        {
            if (id != questionClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionClassExists(questionClass.Id))
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
            return View(questionClass);
        }

        // GET: QuestionClasses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionClass = await _context.QuestionClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionClass == null)
            {
                return NotFound();
            }

            return View(questionClass);
        }

        // POST: QuestionClasses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionClass = await _context.QuestionClass.FindAsync(id);
            _context.QuestionClass.Remove(questionClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionClassExists(int id)
        {
            return _context.QuestionClass.Any(e => e.Id == id);
        }
    }
}

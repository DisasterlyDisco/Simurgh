using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simurgh.Data;
using Simurgh.Models;

namespace Simurgh.Controllers
{
    public class TimeSensitiveTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeSensitiveTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeSensitiveTasks
        public async Task<IActionResult> Index()
        {
              return _context.TimeSensitiveTask != null ? 
                          View(await _context.TimeSensitiveTask.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TimeSensitiveTask'  is null.");
        }

        // GET: ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return _context.TimeSensitiveTask != null ?
                          View("Index", await _context.TimeSensitiveTask.Where(j => j.Title.Contains(SearchPhrase)||j.Description.Contains(SearchPhrase)).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TimeSensitiveTask'  is null.");
        }

        // GET: TimeSensitiveTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeSensitiveTask == null)
            {
                return NotFound();
            }

            var timeSensitiveTask = await _context.TimeSensitiveTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSensitiveTask == null)
            {
                return NotFound();
            }

            return View(timeSensitiveTask);
        }

        // GET: TimeSensitiveTasks/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeSensitiveTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate")] TimeSensitiveTask timeSensitiveTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeSensitiveTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeSensitiveTask);
        }

        // GET: TimeSensitiveTasks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeSensitiveTask == null)
            {
                return NotFound();
            }

            var timeSensitiveTask = await _context.TimeSensitiveTask.FindAsync(id);
            if (timeSensitiveTask == null)
            {
                return NotFound();
            }
            return View(timeSensitiveTask);
        }

        // POST: TimeSensitiveTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate")] TimeSensitiveTask timeSensitiveTask)
        {
            if (id != timeSensitiveTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSensitiveTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeSensitiveTaskExists(timeSensitiveTask.Id))
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
            return View(timeSensitiveTask);
        }

        // GET: TimeSensitiveTasks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeSensitiveTask == null)
            {
                return NotFound();
            }

            var timeSensitiveTask = await _context.TimeSensitiveTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSensitiveTask == null)
            {
                return NotFound();
            }

            return View(timeSensitiveTask);
        }

        // POST: TimeSensitiveTasks/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeSensitiveTask == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TimeSensitiveTask'  is null.");
            }
            var timeSensitiveTask = await _context.TimeSensitiveTask.FindAsync(id);
            if (timeSensitiveTask != null)
            {
                _context.TimeSensitiveTask.Remove(timeSensitiveTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeSensitiveTaskExists(int id)
        {
          return (_context.TimeSensitiveTask?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

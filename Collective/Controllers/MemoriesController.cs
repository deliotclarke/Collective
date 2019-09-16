using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collective.Data;
using Collective.Models;

namespace Collective.Controllers
{
    public class MemoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Memories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Memory.Include(m => m.ApplicationUser).Include(m => m.Record);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Memories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memory = await _context.Memory
                .Include(m => m.ApplicationUser)
                .Include(m => m.Record)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memory == null)
            {
                return NotFound();
            }

            return View(memory);
        }

        // GET: Memories/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist");
            return View();
        }

        // POST: Memories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,ApplicationUserId,Title,MemoryBody")] Memory memory)
        {
            var collection = _context.Collection
                .FirstOrDefault(col => col.RecordId == memory.RecordId && col.ApplicationUserId == memory.ApplicationUserId);                

            memory.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(memory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Collections", new { id = collection.Id });
            }

            return NotFound();
        }

        // GET: Memories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memory = await _context.Memory.FindAsync(id);
            if (memory == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", memory.ApplicationUserId);
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist", memory.RecordId);
            return View(memory);
        }

        // POST: Memories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecordId,ApplicationUserId,MemoryBody,DateAdded")] Memory memory)
        {
            if (id != memory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoryExists(memory.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", memory.ApplicationUserId);
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist", memory.RecordId);
            return View(memory);
        }

        // GET: Memories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memory = await _context.Memory
                .Include(m => m.ApplicationUser)
                .Include(m => m.Record)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memory == null)
            {
                return NotFound();
            }

            return View(memory);
        }

        // POST: Memories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memory = await _context.Memory.FindAsync(id);

            var collection = _context.Collection
                .FirstOrDefault(col => col.RecordId == memory.RecordId && col.ApplicationUserId == memory.ApplicationUserId);

            _context.Memory.Remove(memory);

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Collections", new { id = collection.Id });
        }

        private bool MemoryExists(int id)
        {
            return _context.Memory.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collective.Data;
using Collective.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Collective.Models.CollectionViewModel;

namespace Collective.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly UserManager<ApplicationUser> _userManager;

        public CollectionsController(ApplicationDbContext context, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _config = config;
            _signInManger = signInManager;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Collections
        [Authorize]
        public async Task<IActionResult> Index(bool topFive)
        {
            var currentUser = await GetCurrentUserAsync();

            if (topFive == true)
            {
                var top5 = await _context.Collection
                    .Include(col => col.Record)
                    .Where(col => col.TopFive == true && col.ApplicationUserId == currentUser.Id)
                    .ToListAsync();

                return View(top5);
            }

            ViewData["TopFive"] = topFive;

            var userCollection = _context.Collection
                .Include(co => co.Record)
                .Where(co => co.ApplicationUserId == currentUser.Id)
                .OrderByDescending(co => co.DateAdded)
                .ToList();

            if(userCollection != null)
            { 
                return View(userCollection);
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> PublicCollection(string id, bool topFive)
        {
            var currentUser = await GetCurrentUserAsync();

            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (currentUser.Id == id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (topFive == true)
            {
                var top5 = await _context.Collection
                    .Include(col => col.Record)
                    .Include(col => col.ApplicationUser)
                    .Where(col => col.TopFive == true && col.ApplicationUserId == id)
                    .ToListAsync();

                return View(top5);
            }

            ViewData["TopFive"] = topFive;

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == id);

            var collection = await _context.Collection
                .Include(col => col.Record)
                .Where(col => col.ApplicationUserId == id).ToListAsync();

            return View(collection);
        }

        // GET: Collections/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .Include(c => c.ApplicationUser)
                .Include(c => c.Record)
                .FirstOrDefaultAsync(m => m.Id == id);

            var memories = await _context.Memory
                .Where(m => m.RecordId == collection.RecordId && m.ApplicationUserId == collection.ApplicationUserId)
                .ToListAsync();

            var viewModel = new CollectionDetailMemoryViewModel()
            {
                Collection = collection,
                Record = collection.Record,
                Memories = memories
            };

            if (collection == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist");
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,RecordId,DateAdded,NeedList,TopFive")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", collection.ApplicationUserId);
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist", collection.RecordId);
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", collection.ApplicationUserId);
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist", collection.RecordId);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,RecordId,DateAdded,NeedList,TopFive")] Collection collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", collection.ApplicationUserId);
            ViewData["RecordId"] = new SelectList(_context.Record, "Id", "Artist", collection.RecordId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            var collection = await _context.Collection
                .Include(c => c.Record)
                .Where(c => c.ApplicationUserId == user.Id)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collection.FindAsync(id);
            _context.Collection.Remove(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddTopFive(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            
            var collection = await _context.Collection
                .FirstOrDefaultAsync(col => col.Id == id);

            var top5 = await _context.Collection
                .Where(col => col.TopFive == true && col.ApplicationUserId == currentUser.Id)
                .ToListAsync();

            if (collection != null)
            {
                if (top5.Count < 5)
                {
                    collection.TopFive = true;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Index", new { topfive = true });
            }

            return NotFound();
        }

        public async Task<IActionResult> RemoveTopFive(int id)
        {
            var collection = await _context.Collection
                .FirstOrDefaultAsync(col => col.Id == id);

            if (collection != null)
            {
                collection.TopFive = false;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool CollectionExists(int id)
        {
            return _context.Collection.Any(e => e.Id == id);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collective.Data;
using Collective.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

// This controller will be used for getting records from discogs, not from records inside the Collective database
namespace Collective.Controllers
{
    public class SearchController : Controller
    {
        private readonly string _recordURL = @"https://api.discogs.com/database/search?q=Johnny&format=Vinyl&type=all&key=vLjKQVWwqcRKcmZqrPba&secret=EASZywMOXSWvazmAuYdgvJWfEttkQJDh";
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public SearchController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Search
        public async Task<IActionResult> Index()
        {
            return View(await _context.Record.ToListAsync());
        }

        // GET: Search/Details/5
        public async Task<IActionResult> Details()
        {

            //if (searchString == null)
            //{
            //    return NotFound();
            //}

            var key = _config["Discogs:Key"];
            var secret = _config["Discogs:Secret"];
            var url = $"{_recordURL}";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("user-agent", "Collective");

            var response = await client.GetAsync(url);


            if (response.IsSuccessStatusCode)
            {
                

                var recordString = await response.Content.ReadAsStringAsync();
                var JsonObj = JsonConvert.DeserializeObject(recordString);
                
                if (JsonObj != null)
                {
                    return null;
                };
                //foreach (var record in JsonObj)
                //{
                //    var recordObj = new Record()
                //    {
                //    };
                //}

                //var record = await _context.Record
                //.FirstOrDefaultAsync(m => m.Id == searchString);
                //if (record == null)
                //{
                //    return NotFound();
                //}

                return View();
            }

            return NotFound();
            
        }

        // GET: Search/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Artist,Title,RecordCompany,Condition,TrackList,Barcode")] Record record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(record);
        }

        // GET: Search/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // POST: Search/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Artist,Title,RecordCompany,Condition,TrackList,Barcode")] Record record)
        {
            if (id != record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.Id))
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
            return View(record);
        }

        // GET: Search/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record
                .FirstOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // POST: Search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.Record.FindAsync(id);
            _context.Record.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.Id == id);
        }
    }
}
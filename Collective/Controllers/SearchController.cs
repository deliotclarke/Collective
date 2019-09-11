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
using System.Net.Http;
using Newtonsoft.Json;
using static Collective.Models.DiscogsSearch;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;

// This controller will be used for getting records from discogs, not from records inside the Collective database
namespace Collective.Controllers
{
    public class SearchController : Controller
    {
        private readonly string _recordURL = @"https://api.discogs.com/database/search?q=";
        private readonly string _masterURL = @"https://api.discogs.com/masters/";
        private readonly string _keepItVinyl = @"&format=Vinyl&per_page=10&";
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(ApplicationDbContext context, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _config = config;
            _signInManger = signInManager;
            _userManager = userManager;
        }

        // GET: Search
        public async Task<IActionResult> Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View();
            }

            var discogsResponse = await GetRequestFromDiscogs(searchString);

            if (discogsResponse != null)
            {
                var recordList = discogsResponse.Results;
                return View(recordList);
            }
            return View();
        }

        private async Task<DiscogsPageResponse> GetRequestFromDiscogs(string searchString)
        {
            var key = _config["Discogs:Key"];
            var secret = _config["Discogs:Secret"];
            var query = searchString;
            var vinyl = _keepItVinyl;
            var url = $"{_recordURL}{query}{vinyl}key={key}&secret={secret}";
            var client = new HttpClient();

            // required user-agent header - otherwise response is 403(forbidden)
            client.DefaultRequestHeaders.Add("user-agent", "Collective");

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsAsync<DiscogsPageResponse>();

                if (responseContent != null)
                {
                    return responseContent;
                }
            }
            return null;
        }

        // GET: Search/Details/5
        [Authorize]
        [Route("Search/Details/{masterUrl}/{imageUrl}")]
        public async Task<IActionResult> Details(string masterUrl, string imageUrl)
        {
            if (masterUrl == null)
            {
                return NotFound();
            }

            var newMasterUrl = masterUrl.Replace("%2F", "/");
            var newImageUrl = imageUrl.Replace("%2F", "/");

            var discogsResponse = await GetMasterSearchFromDiscogs(newMasterUrl);

            discogsResponse.ImageUrl = newImageUrl;

            return View(discogsResponse);
        }

        private async Task<DiscogsMasterSearch> GetMasterSearchFromDiscogs(string masterUrl)
        {
            var url = $"{masterUrl}";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("user-agent", "Collective");

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsAsync<DiscogsMasterSearch>();

                return responseContent;
            }

            return null;
        }

        // GET: Search/Add
        [Authorize]
        public async Task<IActionResult> Add(int? id, string imageUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var key = _config["Discogs:Key"];
            var secret = _config["Discogs:Secret"];
            int query = Convert.ToInt32(id);
            var vinyl = _keepItVinyl;
            var url = $"{_masterURL}{query}";
            var client = new HttpClient();

            var newImageUrl = imageUrl.Replace("%2F", "/");

            client.DefaultRequestHeaders.Add("user-agent", "Collective");

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var secondQuery = query.ToString();
                var responseRegularContent = await GetRequestFromDiscogs(secondQuery);
                var responseMasterContent = await response.Content.ReadAsAsync<DiscogsMasterSearch>();   

                var currentUser = await GetCurrentUserAsync();

                var collectionCheck = _context.Collection
                    .Where(col => (col.ApplicationUserId == currentUser.Id) 
                    &&(col.Record.Master_Id == id))
                    .FirstOrDefault();

                if (collectionCheck == null)
                {

                    var singleRegularRecord = responseRegularContent.Results.FirstOrDefault(rec => rec.Master_Id == query);

                    Record record = new Record()
                    {
                        Master_Id = query,
                        Catno = singleRegularRecord.Catno,
                        Thumb = singleRegularRecord.Thumb,
                        Cover_Image = singleRegularRecord.Cover_Image,
                        Artist = responseMasterContent.Artists.First().Name,
                        Title = responseMasterContent.Title,
                        Year = responseMasterContent.Year,
                        TrackList = responseMasterContent.Tracklist,
                        Barcode = singleRegularRecord.Barcode.ToList(),
                        Master_Url = singleRegularRecord.Master_Url
                    };

                    return View(record);
                }

                //this needs to be an error that says the user already has it in their collection
                return NotFound();
            }

            return NotFound();
        }

        // POST: Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Master_Id,Catno,Thumb,Cover_Image,Artist,Title,Year,Label,Condition,TrackList,Barcode, Master_Url")] Record record)
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
            if (id != record.Master_Id)
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
                    if (!RecordExists(record.Master_Id))
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

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.Master_Id == id);
        }
    }
}

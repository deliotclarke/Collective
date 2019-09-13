using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Collective.Models;
using Collective.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Collective.Models.ProfileViewModel;

namespace Collective.Controllers
{

    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _config = config;
            _signInManger = signInManager;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProfileAsync()
        {
            var user = await GetCurrentUserAsync();

            var collection = await _context.Collection
                .Include(col => col.Record)
                .Where(col => col.ApplicationUserId == user.Id).ToListAsync();

            var memories = await _context.Memory
                .Include(mem => mem.Record)
                .Where(mem => mem.ApplicationUserId == user.Id).ToListAsync();

            var viewModel = new ProfileViewModel()
            {
                User = user,
                Collection = collection,
                Memories = memories
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

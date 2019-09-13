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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Collective.Controllers
{

    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;

        public HomeController(ApplicationDbContext context, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _context = context;
            _config = config;
            _signInManger = signInManager;
            _userManager = userManager;
            _env = env;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
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

        [Authorize]
        [HttpPost("ImageUpload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            var user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                user.UserImgPath = await SaveFile(file, user.Id);

                if (user.UserImgPath == null)
                {
                    return NotFound();
                }

                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Profile));
            }

            return NotFound();
        }

        private async Task<string> SaveFile(IFormFile file, string userId)
        {
            if (file.Length > 5242880)
            {
                throw new Exception("File is too large! Try again!");
            }

            var ext = GetFileType(file.FileName);
            if (ext == null)
            {
                throw new Exception("Incorrect file type, try again!");
            }

            var timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            var fileName = $"{timestamp}-{userId}.{ext}";

            var webRoot = _env.WebRootPath;
            var filePath = Path.Combine(webRoot, "images", fileName);

            string relFilePath = null;
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    relFilePath = $"~/images/{fileName}";
                }
            }

            return relFilePath;
        }

        private string GetFileType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            string contentType;
            provider.TryGetContentType(fileName, out contentType);

            if (contentType == "image/jpeg")
            {
                contentType = "jpg";
            }
            else
            {
                contentType = null;
            }

            return contentType;
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

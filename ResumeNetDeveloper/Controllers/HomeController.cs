using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ResumeNetDeveloper.Data;
using ResumeNetDeveloper.Models;
using System.Diagnostics;

namespace ResumeNetDeveloper.Controllers
{
    [Authorize]
    public class HomeController : BaseController<HomeController>
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context, IStringLocalizer<HomeController> localizer) : base(localizer)
        {
            _logger = logger;
            _userManager = userManager;

            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? employerId)
        {
            // ѕолучение списка всех работодателей
            var employers = _context.Employers.ToList();
            ViewBag.Employers = employers;
            ViewBag.SelectedEmployerId = employerId;

            // ѕолучение навыков с учетом выбранного работодател€
            var skills = _context.Skills
                                 .Include(s => s.EmployerSkills)
                                 .ThenInclude(es => es.Employer)
                                 .AsQueryable();

            if (employerId.HasValue)
            {
                skills = skills.Where(s => s.EmployerSkills.Any(es => es.EmployerId == employerId));
            }

            return View(skills.ToList());
        }

        /*
        [HttpGet]
        public IActionResult Index(int? employerId)
        {
            var employers = _context.Employers.ToList();
            ViewBag.Employers = employers;
            ViewBag.SelectedEmployerId = employerId;

            var skills = _context.Skills
                                 .Include(s => s.EmployerSkills)
                                 .ThenInclude(es => es.Employer)
                                 .AsQueryable();

            if (employerId.HasValue)
            {
                skills = skills.Where(s => s.EmployerSkills.Any(es => es.EmployerId == employerId));
            }

            return View(skills.ToList());
        }
        */
        [Authorize]
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Hello()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["UserEmail"] = user.Email;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using StudentGymKOI.Data;
using StudentGymKOI.Models;
using System.Diagnostics;

namespace StudentGymKOI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                

                

                var userId = user.Id;
                var userMemberships = await _context.UserMembership
                .Where(um => um.UserID == userId)
                .ToListAsync();

                var membershipIds = userMemberships.Select(um => um.MembershipID).ToList();

                var memberships = await _context.Membership
                    .Where(m => membershipIds.Contains(m.MembershipID))
                    .ToListAsync();


                ViewBag.Memberships = memberships;
                
            }
           


            return View();
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
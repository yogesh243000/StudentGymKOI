using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentGymKOI.Data;
using StudentGymKOI.Models;

namespace StudentGymKOI.Controllers
{
    public class UserGymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserGymClassesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Member")]

        public async Task<IActionResult> JoinGymClass(int gymClassID)
        {
            var user = await _userManager.GetUserAsync(User);

            var userGymClass = new UserGymClass { UserID = user.Id, GymClassID = gymClassID };

            _context.UserGymClass.Add(userGymClass);
            await _context.SaveChangesAsync();

            TempData["JoinSuccessMessage"] = "You have successfully joined the Gym class.";



            return RedirectToAction("Index", "Home");


        }

    }
}

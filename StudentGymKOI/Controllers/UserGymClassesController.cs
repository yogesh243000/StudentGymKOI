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

            // Find the GymClass entity by gymClassID
            var gymClass = await _context.GymClass.FindAsync(gymClassID);

            if (gymClass != null)
            {
                // Check if the class is full before adding the user
                if (gymClass.CurrentMembers < gymClass.MaxMembers)
                {
                    var userGymClass = new UserGymClass { UserID = user.Id, GymClassID = gymClassID };

                    _context.UserGymClass.Add(userGymClass);

                    // Update the CurrentMembers count
                    gymClass.CurrentMembers++;

                    // Save changes to both UserGymClass and GymClass
                    await _context.SaveChangesAsync();

                    TempData["JoinSuccessMessage"] = "You have successfully joined the Gym class.";
                }
               
            }
           

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LeaveGymClass(int gymClassID)
        {
            var user = await _userManager.GetUserAsync(User);

            var userGymClass = _context.UserGymClass.FirstOrDefault(ugc => ugc.UserID == user.Id && ugc.GymClassID == gymClassID);
            var gymClass = await _context.GymClass.FindAsync(gymClassID); 
            

            if (userGymClass != null && gymClass != null)
            {
                _context.UserGymClass.Remove(userGymClass);
                gymClass.CurrentMembers--;
                await _context.SaveChangesAsync();
            }

            TempData["LeaveSuccessMessage"] = "You have successfully left the gym class.";

            return RedirectToAction("Index", "Home");


        }

    }
}

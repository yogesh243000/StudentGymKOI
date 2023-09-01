using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGymKOI.Data;
using StudentGymKOI.Models;

namespace StudentGymKOI.Controllers
{
    public class UserMembershipController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;



        public UserMembershipController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> JoinMembership(int membershipID)
        {
            // Get the current logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Create a new UserMembership record
            var userMembership = new UserMembership
            {
                UserID = user.Id,
                MembershipID = membershipID,
                
            };

            var previous = _context.UserMembership.FirstOrDefault(um => um.UserID == user.Id);

            if (previous != null)
            {
                _context.UserMembership.Remove(previous);
            }


            // Add the UserMembership record to the database
            _context.UserMembership.Add(userMembership);
            await _context.SaveChangesAsync();

            TempData["JoinSuccessMessage"] = "You have successfully joined the membership.";

            // Redirect to a success page or back to the membership selection page
            return RedirectToAction("Index", "Home");
        }

    }
}

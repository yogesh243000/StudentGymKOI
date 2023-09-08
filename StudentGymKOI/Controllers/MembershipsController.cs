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
    
    public class MembershipsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public MembershipsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Memberships
        [Authorize]
        public async Task<IActionResult> Index()
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

            var userMembership = memberships.FirstOrDefault();
            ViewBag.UserMembership = userMembership;

            ViewBag.Memberships = await _context.Membership.ToListAsync();




            return _context.Membership != null ? 
                          View() :
                          Problem("Entity set 'ApplicationDbContext.Membership'  is null.");
        }

        // GET: Memberships/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .FirstOrDefaultAsync(m => m.MembershipID == id);
            if (membership == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var userMemberships = await _context.UserMembership
               .Where(um => um.UserID == userId)
               .ToListAsync();

            var membershipIds = userMemberships.Select(um => um.MembershipID).ToList();

            var memberships = await _context.Membership
                .Where(m => membershipIds.Contains(m.MembershipID))
                .ToListAsync();




            var userMembership = memberships.FirstOrDefault();
            ViewBag.UserMembership = userMembership;


            return View(membership);
        }

        // GET: Memberships/Create
        [Authorize(Roles = "Gym Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gym Staff")]
        public async Task<IActionResult> Create([Bind("MembershipID,MemberShipName,Price")] Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membership);
        }

        // GET: Memberships/Edit/5
        [Authorize(Roles = "Gym Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gym Staff")]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipID,MemberShipName,Price")] Membership membership)
        {
            if (id != membership.MembershipID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.MembershipID))
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
            return View(membership);
        }

        // GET: Memberships/Delete/5
        [Authorize(Roles = "Gym Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .FirstOrDefaultAsync(m => m.MembershipID == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gym Staff")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membership == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Membership'  is null.");
            }
            var membership = await _context.Membership.FindAsync(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(int id)
        {
          return (_context.Membership?.Any(e => e.MembershipID == id)).GetValueOrDefault();
        }
    }
}

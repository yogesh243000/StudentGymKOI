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
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GymClassesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        // GET: GymClasses
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var userGymClasses = await _context.UserGymClass
                .Where(ugc => ugc.UserID == userId)
                .ToListAsync();

            var gymClassIds = userGymClasses.Select(ugc => ugc.GymClassID).ToList();

            var gymClasses = await _context.GymClass
                .Where(gc => gymClassIds.Contains(gc.Id))
                .ToListAsync();

            ViewBag.UserGymClasses = gymClasses;

            

           

            ViewBag.GymClasses = await _context.GymClass.ToListAsync();
              return _context.GymClass != null ? 
                          View() :
                          Problem("Entity set 'ApplicationDbContext.GymClass'  is null.");
        }

        [Authorize]
        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GymClass == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var userGymClasses = await _context.UserGymClass
                .Where(ugc => ugc.UserID == userId)
                .ToListAsync();

            var gymClassIds = userGymClasses.Select(ugc => ugc.GymClassID).ToList();

            var gymClasses = await _context.GymClass
                .Where(gc => gymClassIds.Contains(gc.Id))
                .ToListAsync();

            ViewBag.UserGymClasses = gymClasses;

            return View(gymClass);
        }

        // GET: GymClasses/Create
        [Authorize(Roles = "Gym Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gym Staff")]

        public async Task<IActionResult> Create([Bind("Id,ClassName,MaxMembers")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        [Authorize(Roles = "Gym Staff")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GymClass == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClass.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gym Staff")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,MaxMembers,CurrentMembers")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        [Authorize(Roles = "Gym Staff")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GymClass == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gym Staff")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GymClass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GymClass'  is null.");
            }
            var gymClass = await _context.GymClass.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClass.Remove(gymClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
          return (_context.GymClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

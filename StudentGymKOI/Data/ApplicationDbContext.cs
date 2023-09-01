using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentGymKOI.Models;

namespace StudentGymKOI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentGymKOI.Models.Membership>? Membership { get; set; }
        public DbSet<StudentGymKOI.Models.UserMembership>? UserMembership { get; set; }
    }
}
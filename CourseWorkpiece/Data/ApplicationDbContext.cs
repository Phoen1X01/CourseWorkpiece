using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using CourseWorkpiece.Models;
namespace CourseWorkpiece.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<sGroup> sGroups { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Traffic> Traffics { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sGroup>()
                .HasOne(u => u.User) //иметь 1 группу
                .WithOne(g => g.sGroup) //с 1 юзером
                .HasForeignKey<Group>(u => Users);
                

        }

    }
}

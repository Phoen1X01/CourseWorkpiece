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
                .HasOne(g => g.User) //имеет 1 юзера
                .WithOne(g => g.sGroup) //с 1 группой
                .HasForeignKey<User>(u => u.sGroupId); //по ключу у юзера groupID

            modelBuilder.Entity<Student>()
                .HasOne(s => s.sGroup) //имеет 1 группу
                .WithMany(g => g.Students) //с многими Students
                .HasForeignKey(s => s.sGroupId);
            //это связь 1 ко многим



        }

    }
}

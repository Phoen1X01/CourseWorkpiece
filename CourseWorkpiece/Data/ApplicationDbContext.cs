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

        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sGroup>()
                .HasOne(g => g.User) //имеет 1 юзера
                .WithOne(u => u.sGroup) //с 1 группой
                .HasForeignKey<User>(u => u.sGroupId); //по ключу у юзера groupID
            // 'nj cdzpm 1 r 1
            modelBuilder.Entity<Student>()
                .HasOne(s => s.sGroup) //имеет 1 группу
                .WithMany(g => g.Students) //с многими Students
                .HasForeignKey(s => s.sGroupId); //ключ у студентов
            //это связь 1 ко многим
            modelBuilder.Entity<Traffic>()
                .HasOne(t => t.Student)
                .WithMany(s => s.Traffics)
                .HasForeignKey(t => t.StudentId);

            modelBuilder.Entity<Lecture>()
                .HasMany(l => l.Traffics)
                .WithOne(t => t.Lecture)
                .HasForeignKey(t => t.LectureId);

            modelBuilder.Entity<Session>()
                .HasOne(w => w.User)
                .WithMany(r => r.Sessions)
                .HasForeignKey(y => y.UserId);
                


        }

    }
}

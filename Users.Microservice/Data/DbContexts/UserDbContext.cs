using Microsoft.EntityFrameworkCore;
using Users.Microservice.Models.Entities;

namespace Users.Microservice.Data.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<SavedCourse> SavedCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Region)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Image)
                .WithMany()
                .HasForeignKey(c => c.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

            // saved courses
            modelBuilder.Entity<SavedCourse>()
                .HasKey(sc => new { sc.CourseId, sc.UserId });

            modelBuilder.Entity<SavedCourse>()
                .HasOne(sc => sc.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<SavedCourse>()
                .HasOne(sc => sc.Course)
                .WithMany()
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Author)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using CodingCourses.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingCourses.DataAccess.Contracts.Contexts
{
    public class CodingCoursesDbContext : DbContext
    {
        public CodingCoursesDbContext()
        { }

        public CodingCoursesDbContext(DbContextOptions<CodingCoursesDbContext> options) : base(options)
        { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Topic>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Topics)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(Environment.GetEnvironmentVariable("CodingCoursesConnection"));
    }
}

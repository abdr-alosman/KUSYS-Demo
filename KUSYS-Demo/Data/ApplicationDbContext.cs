using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table Per Type(TPT)
            modelBuilder.Entity<Student>().ToTable("StudentUsers");
            modelBuilder.Entity<User>().Property(b => b.NameSurname).HasComputedColumnSql("[FirstName]+' '+[LastName]");
            base.OnModelCreating(modelBuilder);
        }
        

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public static readonly ILoggerFactory MyloggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(MyloggerFactory)
                    .UseSqlServer("Server=YAZILIM001;Database=aspnet-KUSYSDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
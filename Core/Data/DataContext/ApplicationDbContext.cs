using Core.Data.Entities;
using Core.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Core.Data.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        //public DbSet<Role> Role { get; set; }
        //public DbSet<ActivityLog> ActivityLog { get; set; }
        //public DbSet<User> User { get; set; }
        //public DbSet<RefreshToken> RefreshToken { get; set; }
        //public DbSet<Permission> Permission { get; set; }
        //public DbSet<Branch> Branch { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(Config.config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
                //optionsBuilder.UseSqlServer("Server=MSF-ERP\\MSSQL2017;Database=MSFCO;User ID=sa;Password=Abc1234;MultipleActiveResultSets=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            //builder.Entity<Role>().ToTable($"tbl{nameof(Role)}");
            //builder.Entity<ActivityLog>().ToTable($"tbl{nameof(ActivityLog)}");
            //builder.Entity<User>().ToTable($"tbl{nameof(User)}");
            //builder.Entity<RefreshToken>().ToTable($"tbl{nameof(RefreshToken)}");
            //builder.Entity<Permission>().ToTable($"tbl{nameof(Permission)}");
            //builder.Entity<Branch>().ToTable($"tbl{nameof(Branch)}");
        }
    }
}
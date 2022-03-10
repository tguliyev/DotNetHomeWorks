using BaseProject;
using EFcorePlugin.EntityFrameworkConf.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EFcorePlugin.EntityFrameworkConf
{
    public class DataContext : DbContext
    {
        private const string CONNECTION_STRING = "Data Source=DESKTOP-C9MI1JL;Database=UserApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DataContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
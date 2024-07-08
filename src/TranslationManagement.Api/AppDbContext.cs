using Microsoft.EntityFrameworkCore;

namespace TranslationManagement.Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<TranslationJob> TranslationJobs { get; set; }
        public DbSet<TranslatorModel> Translators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<TranslationJob>().ToTable("TranslationJob");
        }
    }
}
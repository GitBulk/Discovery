using AzureCoreOne.Models;
using AzureCoreOne.Models.CustomerManagement;
using AzureCoreOne.Models.Parsley;
using AzureCoreOne.Models.Quizs;
using AzureCoreOne.ViewModels.Parsley;
using Microsoft.EntityFrameworkCore;

namespace AzureCoreOne.AppContexts
{
    public class TamContext : AzureCoreOneUserContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SkiCard> SkiCards { get; set; }
        public DbSet<PassType> PassTypes { get; set; }
        public DbSet<Pass> Passes { get; set; }
        public DbSet<PassActivation> PassActivations { get; set; }
        public DbSet<Scan> Scans { get; set; }


        // Best practice is to allow the options to be
        // passed into a constructor so that we remove any
        // assumptions about where the data is stored: inmemory,
        // SQL Server, and so on.
        public TamContext(DbContextOptions options) :
        base(options)
        { }
        protected override void OnModelCreating(ModelBuilder
        modelBuilder)
        {
            modelBuilder.Entity<Quiz>().HasMany<Question>().WithOne(q => q.Quiz);
            modelBuilder.Entity<PassTypeResort>().HasKey(p => new { p.PassTypeId, p.ResortId });
            base.OnModelCreating(modelBuilder);
        }
    }
}

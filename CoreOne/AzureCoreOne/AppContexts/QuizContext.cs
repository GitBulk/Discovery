using AzureCoreOne.Models.Quizs;
using Microsoft.EntityFrameworkCore;

namespace AzureCoreOne.AppContexts
{
    public class QuizContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        // Best practice is to allow the options to be
        // passed into a constructor so that we remove any
        // assumptions about where the data is stored: inmemory,
        // SQL Server, and so on.
        public QuizContext(DbContextOptions options) :
        base(options)
        { }
        protected override void OnModelCreating(ModelBuilder
        modelBuilder)
        {
            modelBuilder.Entity<Quiz>().HasMany<Question>
            ().WithOne(q => q.Quiz);
            base.OnModelCreating(modelBuilder);
        }
    }
}

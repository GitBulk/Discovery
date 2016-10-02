using Microsoft.EntityFrameworkCore;

namespace UndoRedo.Models
{
    public class DomainModelContext : DbContext
    {
        public DomainModelContext(DbContextOptions<DomainModelContext> options)
            :base(options)
        { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AboutData> AboutData { get; set; }

        public DbSet<HomeData> HomeData { get; set; }

        public DbSet<CommandEntity> CommandEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutData>().HasKey(m => m.Id);
            modelBuilder.Entity<HomeData>().HasKey(m => m.Id);
            modelBuilder.Entity<CommandEntity>().HasKey(m => m.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}

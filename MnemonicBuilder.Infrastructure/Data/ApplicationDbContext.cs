using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MnemonicBuilder.Domain.Entities;
using MnemonicBuilder.Infrastructure.Entities;

namespace MnemonicBuilder.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Sentence> Sentences => Set<Sentence>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sentence>().ToTable("sentences");
        }
    }
}

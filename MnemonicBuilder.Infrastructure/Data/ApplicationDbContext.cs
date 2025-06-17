using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MnemonicBuilder.Infrastructure.Entities;

namespace MnemonicBuilder.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Sentence> Sentences => Set<Sentence>();
        public DbSet<Set> Sets => Set<Set>();
        public DbSet<SetSentence> SetSentences => Set<SetSentence>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sentence>().ToTable("sentences");

            modelBuilder.Entity<Set>().ToTable("sets");

            modelBuilder.Entity<SetSentence>()
                .HasKey(ss => new { ss.SetId, ss.SentenceId });

            modelBuilder.Entity<SetSentence>()
                .HasOne(ss => ss.Set)
                .WithMany(s => s.SetSentences)
                .HasForeignKey(ss => ss.SetId);

            modelBuilder.Entity<SetSentence>()
                .HasOne(ss => ss.Sentence)
                .WithMany()
                .HasForeignKey(ss => ss.SentenceId);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NomHadopi.Models.EF
{
    public class NameSuggestDbContext : DbContext
    {
        public NameSuggestDbContext()
        {

        }

        public NameSuggestDbContext(DbContextOptions<NameSuggestDbContext> options) : base(options)
        {

        }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<UpVote> UpVotes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suggestion>()
                .HasIndex(b => b.UserIP);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               
            }
        }
    }
}

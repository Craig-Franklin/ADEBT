using ADEBT.Api.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADEBT.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Debt> Debts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Debt>()
                .HasOne(d => d.User)
                .WithMany(d => d.Debts)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

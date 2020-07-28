using Feedc.Domain.UserManagement;
using Feedc.Domain.PersonManagement;
using Microsoft.EntityFrameworkCore;

namespace Feedc.Infrastructure.Database
{
    public class FeedcContext : DbContext
    {
        protected override void OnConfiguring(
         DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=DESKTOP-KCSUK0G\BIDZINASQL; Database=Feedc; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLogin>().ToTable("UserLogin");
            builder.Entity<User>()
                .HasMany(user => user.Persons)
                .WithOne(person => person.User);

            builder.Entity<UserClaim>().ToTable("UserClaim");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Person>().ToTable("Person");
            builder.Entity<UserRole>().ToTable("UserRole");
        }
    }
}

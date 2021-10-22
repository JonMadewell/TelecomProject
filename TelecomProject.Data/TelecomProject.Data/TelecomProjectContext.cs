using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telecom.Domain;

namespace TelecomProject.Data
{
    public class TelecomProjectContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Login> logins { get; set; }

        public TelecomProjectContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One Login per One Person
            modelBuilder.Entity<Person>()
                .HasOne<Login>(p => p.Login)
                .WithOne(l => l.Person)
                .HasForeignKey<Login>(l => l.PersonId);

            //One Account Per One Person
            modelBuilder.Entity<Person>()
                .HasOne<Account>(p => p.Account)
                .WithOne(a => a.Person)
                .HasForeignKey<Account>(a => a.PersonId);

            //Many Devices to Many People
            modelBuilder.Entity<Person>()
                .HasMany<Device>(p => p.Devices)
                .WithMany(d => d.People)
                .UsingEntity<PersonDevice>(pd => pd.HasOne<Device>().WithMany(),
                                           pd => pd.HasOne<Person>().WithMany());

            //Many Plans to Many Accounts
            modelBuilder.Entity<Account>()
                .HasMany<Plan>(a => a.plans)
                .WithMany(pl => pl.Accounts)
                .UsingEntity<AccountPlans>(ap => ap.HasOne<Plan>().WithMany(),
                                           ap => ap.HasOne<Account>().WithMany());

        }
    }
}

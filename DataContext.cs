using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//Even with this setup, any attempt at adding data to the database results in the DB not updating at all. Which is where the problem stopped us.
namespace RunnerGame
{
    internal class DataContext : DbContext
    {
        public DbSet<DbScore> Messages { get; set; }
        

        // Book example
        //public DataContext CreateDbContext(string[] args)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        //    var connectionString = "Server=db.meorung.me;User ID=myth;Password=shuba!;Initial Catalog=CS371;MultipleActiveResultSets=true";
        //    optionsBuilder.UseSqlServer(connectionString);
        //    return new DataContext(optionsBuilder.Options);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //// build many to many relationship
            //modelBuilder.Entity<DbUser>()
            //    .HasMany(u => u.Groups)
            //    .WithMany(g => g.Users)
            //    .UsingEntity(j => j.ToTable("group_user"));

        
        }

        // Microsoft documentation
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=db.meorung.me;User ID=GroupC371;Password=adang24;Initial Catalog=CS371GroupC;MultipleActiveResultSets=true");
        }
    }
}

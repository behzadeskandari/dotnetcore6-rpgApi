using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(new Skill { Id = 1, Name = "FireBall", Damage = 30 },
                new Skill { Id = 2, Name = "Frebzy", Damage = 40 },
                new Skill { Id = 3, Name = "Bilizard", Damage = 50 });

        }
        //public DbSet<Character> Character => Set<Character>();
        public DbSet<Character> Character { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        

    }
}

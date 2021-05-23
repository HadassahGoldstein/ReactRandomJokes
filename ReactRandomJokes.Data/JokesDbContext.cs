using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactRandomJokes.Data
{
    public class JokesDbContext: DbContext
    {
        private readonly string _connectionString;
        public JokesDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<UserLikedJokes>()
                .HasKey(ulj => new { ulj.JokeId, ulj.UserId });

            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ulj => ulj.User)
                .WithMany(u => u.UserLikedJokes)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ulj => ulj.Joke)
                .WithMany(j => j.UserLikedJokes)
                .HasForeignKey(j => j.JokeId);         

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLikedJokes> UserLikedJokes { get; set; }
    }
}

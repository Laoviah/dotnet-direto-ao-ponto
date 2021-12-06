using DevGames.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistance
{
    public class DevGameContext : DbContext
    {
        public DevGameContext(DbContextOptions<DevGameContext> options) : base(options)
        {

        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                        .HasKey(b => b.Id);

            modelBuilder.Entity<Board>()
                        .HasMany(b => b.Posts)
                        .WithOne()
                        .HasForeignKey(b => b.BoardId);

            modelBuilder.Entity<Post>()
                        .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                        .HasMany(b => b.Comments)
                        .WithOne()
                        .HasForeignKey(b => b.PostId);

            modelBuilder.Entity<Comment>()
                        .HasKey(c => c.Id);


        }
    }
}

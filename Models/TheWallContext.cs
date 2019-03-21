using Microsoft.EntityFrameworkCore;

namespace TheWall.Models {
    public class TheWallContext : DbContext {
        public TheWallContext (DbContextOptions<TheWallContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
 
namespace weddingplanner.Models
{
    public class weddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public weddingContext(DbContextOptions<weddingContext> options) : base(options) { }
            public DbSet<Userrecord> user { get; set; }
            public DbSet<weddingrecords> weddings { get; set; }
            public DbSet<Invitationsinfo> invitations { get; set; }


    }
}
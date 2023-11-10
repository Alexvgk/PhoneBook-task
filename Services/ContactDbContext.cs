using Microsoft.EntityFrameworkCore;
using PhoneBook_task.Models;

namespace PhoneBook_task.Services
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) :base(options)
        {
            Database.EnsureDeleted();  
            Database.EnsureCreated();   
        }

        public DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

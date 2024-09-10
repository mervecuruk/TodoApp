using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoAppNew.Models;

namespace TodoAppNew.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<CompletedToDoItem> CompletedToDoItems { get; set; }
    }
}

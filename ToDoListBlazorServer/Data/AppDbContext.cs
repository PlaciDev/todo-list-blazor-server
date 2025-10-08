using Microsoft.EntityFrameworkCore;
using ToDoListBlazorServer.Models;


namespace ToDoListBlazorServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users {get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}

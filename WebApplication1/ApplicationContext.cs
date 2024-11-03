using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


public class ApplicationDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
using Microsoft.EntityFrameworkCore;
using API_LibraryManagement.Models;

namespace API_LibraryManagement.Persistence.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}

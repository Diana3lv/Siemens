namespace API_LibraryManagement.Persistence.Repositories;

using API_LibraryManagement.Models;
using API_LibraryManagement.Persistence.Data;
using API_LibraryManagement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

public class TransactionRepository : ITransactionRepository
{
    private readonly LibraryDbContext _context;

    public TransactionRepository(LibraryDbContext context)
    {
        _context = context;
    }
    
    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
    
    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.Include(t => t.Book).ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions.Include(t => t.Book).FirstOrDefaultAsync(t => t.Id == id);
    }
}

namespace API_LibraryManagement.Application.Interfaces;

using API_LibraryManagement.Models;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(int id);
}

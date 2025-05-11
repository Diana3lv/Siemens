namespace API_LibraryManagement.Application.Interfaces;

using API_LibraryManagement.Models;

public interface ITransactionService
{
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(int id);
}

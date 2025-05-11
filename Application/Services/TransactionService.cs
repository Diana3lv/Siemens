namespace API_LibraryManagement.Application.Services;

using API_LibraryManagement.Models;
using API_LibraryManagement.Application.Interfaces;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        transaction.Timestamp = DateTime.UtcNow;
        return await _repository.AddAsync(transaction);
    }

    public Task<IEnumerable<Transaction>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Transaction?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
}

using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task<Transaction?> GetByIdAsync(Guid transactionId);
    Task AddAsync(Transaction transaction);
}
using Microsoft.EntityFrameworkCore;
using TourismAgencyAPI.Data;
using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Repositories;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        return await context.Transactions
            .Include(t => t.User)
            .Include(t => t.Package)
            .ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(Guid transactionId)
    {
        return await context.Transactions
            .Include(t => t.User)
            .Include(t => t.Package)
            .FirstOrDefaultAsync(t => t.Id == transactionId);
    }

    public async Task AddAsync(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();
    }
}
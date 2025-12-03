using ForceGet.Domain.Entities;

namespace ForceGet.Domain.Interfaces;

public interface IQuoteRepository : IRepository<Quote>
{
    Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId);
}

using CNA.Application.Interfaces;

namespace CNA.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CNADbContext _context;

        public UnitOfWork(CNADbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}

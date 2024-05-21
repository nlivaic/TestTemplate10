using System.Threading.Tasks;
using TestTemplate10.Common.Interfaces;

namespace TestTemplate10.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTemplate10DbContext _dbContext;

        public UnitOfWork(TestTemplate10DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }
    }
}
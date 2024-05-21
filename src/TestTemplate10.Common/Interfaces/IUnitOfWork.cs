using System.Threading.Tasks;

namespace TestTemplate10.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
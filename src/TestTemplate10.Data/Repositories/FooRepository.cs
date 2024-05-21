using TestTemplate10.Core.Entities;
using TestTemplate10.Core.Interfaces;

namespace TestTemplate10.Data.Repositories
{
    public class FooRepository : Repository<Foo>, IFooRepository
    {
        public FooRepository(TestTemplate10DbContext context)
            : base(context)
        {
        }
    }
}

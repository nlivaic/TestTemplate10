using System.Threading.Tasks;
using MassTransit;
using TestTemplate10.Core.Events;

namespace TestTemplate10.WorkerServices.FooService
{
    public class FooConsumer : IConsumer<IFooEvent>
    {
        public Task Consume(ConsumeContext<IFooEvent> context) =>
            Task.CompletedTask;
    }
}

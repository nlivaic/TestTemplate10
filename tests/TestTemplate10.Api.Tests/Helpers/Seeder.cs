using System.Collections.Generic;
using TestTemplate10.Core.Entities;
using TestTemplate10.Data;

namespace TestTemplate10.Api.Tests.Helpers
{
    public static class Seeder
    {
        public static void Seed(this TestTemplate10DbContext ctx)
        {
            ctx.Foos.AddRange(
                new List<Foo>
                {
                    new ("Text 1"),
                    new ("Text 2"),
                    new ("Text 3")
                });
            ctx.SaveChanges();
        }
    }
}

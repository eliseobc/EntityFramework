// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.FunctionalTests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.EntityFrameworkCore.InMemory.FunctionalTests
{
    public class GuidValueGeneratorEndToEndTest
    {
        [Fact]
        public async Task Can_use_GUIDs_end_to_end_async()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddInMemoryDatabase()
                .ServiceCollection()
                .BuildServiceProvider();

            var guids = new List<Guid>();
            var guidsHash = new HashSet<Guid>();
            using (var context = new BronieContext(serviceProvider))
            {
                for (var i = 0; i < 10; i++)
                {
                    guids.Add(context.Add(new Pegasus { Name = "Rainbow Dash " + i }).Entity.Id);
                    guidsHash.Add(guids.Last());
                }

                await context.SaveChangesAsync();
            }

            Assert.Equal(10, guidsHash.Count);

            using (var context = new BronieContext(serviceProvider))
            {
                var pegasuses = await context.Pegasuses.OrderBy(e => e.Name).ToListAsync();

                for (var i = 0; i < 10; i++)
                {
                    Assert.Equal(guids[i], pegasuses[i].Id);
                }
            }
        }

        private class BronieContext : DbContext
        {
            public BronieContext(IServiceProvider serviceProvider)
                : base(serviceProvider)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseInMemoryDatabase();

            public DbSet<Pegasus> Pegasuses { get; set; }
        }

        private class Pegasus
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}

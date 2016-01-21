// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Data.Entity.InMemory.FunctionalTests
{
    public class NotificationEntitiesInMemoryTest
        : NotificationEntitiesTestBase<NotificationEntitiesInMemoryTest.NotificationEntitiesInMemoryFixture>
    {
        public NotificationEntitiesInMemoryTest(NotificationEntitiesInMemoryFixture fixture)
            : base(fixture)
        {
        }

        public class NotificationEntitiesInMemoryFixture : NotificationEntitiesFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly DbContextOptions _options;

            public NotificationEntitiesInMemoryFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFramework()
                    .AddInMemoryDatabase()
                    .ServiceCollection()
                    .AddSingleton(TestInMemoryModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();

                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseInMemoryDatabase();
                _options = optionsBuilder.Options;

                EnsureCreated();
            }

            public override DbContext CreateContext()
                => new DbContext(_serviceProvider, _options);
        }
    }
}

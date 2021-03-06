// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests.TestModels
{
    public class SqlServerNorthwindContext : NorthwindContext
    {
        public static readonly string DatabaseName = StoreName;
        public static readonly string ConnectionString = SqlServerTestStore.CreateConnectionString(DatabaseName);

        public SqlServerNorthwindContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        public static SqlServerTestStore GetSharedStore()
        {
            return SqlServerTestStore.GetOrCreateShared(
                DatabaseName,
                () => SqlServerTestStore.CreateDatabase(DatabaseName, scriptPath: @"Northwind.sql"));
        }
    }
}

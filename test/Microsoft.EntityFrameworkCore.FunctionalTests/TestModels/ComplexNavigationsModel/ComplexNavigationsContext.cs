// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.ComplexNavigationsModel
{
    public class ComplexNavigationsContext : DbContext
    {
        public static readonly string StoreName = "ComplexNavigations";

        public ComplexNavigationsContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        public DbSet<Level1> LevelOne { get; set; }
        public DbSet<Level2> LevelTwo { get; set; }
        public DbSet<Level3> LevelThree { get; set; }
        public DbSet<Level4> LevelFour { get; set; }

        public DbSet<ComplexNavigationField> Fields { get; set; }
        public DbSet<ComplexNavigationString> MultilingualStrings { get; set; }
        public DbSet<ComplexNavigationGlobalization> Globalizations { get; set; }
        public DbSet<ComplexNavigationLanguage> Languages { get; set; }
    }
}

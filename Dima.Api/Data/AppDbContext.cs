﻿using Dima.Api.Models;
using Dima.Core.Models;
using Dima.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Dima.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext
        <
            User,IdentityRole<long>, long, IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>,
            IdentityRoleClaim<long>, IdentityUserToken<long>>(options)
    {

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<IncomesAndExpenses> IncomesAndExpenses { get; set; } = null!;  
        public DbSet<IncomesByCategory> IncomesByCategories { get; set; } = null!;
        public DbSet<ExpensesByCategory> ExpensesByCategories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            model.Entity<IncomesAndExpenses>().HasNoKey().ToView("vwGetIncomesAndExpenses");
            model.Entity<IncomesByCategory>().HasNoKey().ToView("vwIncomesByCategory");
            model.Entity<ExpensesByCategory>().HasNoKey().ToView("vwGetExpensesByCategory");
        }
    }
}

using System.Linq.Expressions;
using System.Reflection;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

using Server.AccountTypes;
using Server.Budgets;
using Server.Common.Entities;
using Server.Expenses;
using Server.Organizations;
using Server.Users;

namespace Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        private readonly CurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions options, CurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<UserOrganization> UserOrganizations { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseAccount> ExpenseAccounts { get; set; }

        public DbSet<BudgetTemplate> BudgetTemplates { get; set; }

        public DbSet<BudgetHeader> BudgetHeaders { get; set; }

        public DbSet<BudgetAccount> BudgetAccounts { get; set; }

        public DbSet<BudgetTotalExpensesAndBudget> BudgetTotalExpensesAndBudget { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {

                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        if (_currentUserService.UserId is not null)
                        {
                            entry.Entity.UserId = (int)_currentUserService.UserId;
                        }

                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }

}
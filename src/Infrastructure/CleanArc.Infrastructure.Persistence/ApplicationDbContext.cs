using System.Reflection;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArc.Infrastructure.Persistence;

public class ApplicationDbContext: IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
        base.SavingChanges += OnSavingChanges;
    }
    
    private void OnSavingChanges(object sender, SavingChangesEventArgs e)
    {
        _cleanString();
        // NOT NEED as Interceptor in place
        // ConfigureEntityDates();
    }

    private void _cleanString()
    {
        var changedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
                continue;

            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var val = (string)property.GetValue(item.Entity, null);

                if (val.HasValue())
                {
                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val)
                        continue;
                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntity).Assembly;
        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddPluralizingTableNameConvention();


    }

    private void ConfigureEntityDates()
    {
        var updatedEntities = ChangeTracker.Entries().Where(x =>
            x.Entity is IAuditable && x.State == EntityState.Modified).Select(x => x.Entity as IAuditable);

        var addedEntities = ChangeTracker.Entries().Where(x =>
            x.Entity is IAuditable && x.State == EntityState.Added).Select(x => x.Entity as IAuditable);

        foreach (var entity in updatedEntities)
        {
            if (entity != null)
            {
                entity.UpdatedOn = DateTime.Now;
            }
        }

        foreach (var entity in addedEntities)
        {
            if (entity != null)
            {
                entity.CreatedOn = DateTime.Now;
                entity.UpdatedOn = DateTime.Now;
            }
        }
    }
}
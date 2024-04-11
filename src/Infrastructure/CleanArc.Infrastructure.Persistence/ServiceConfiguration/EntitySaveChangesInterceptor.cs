using CleanArc.SharedKernel.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Domain.Common;

namespace CleanArc.Infrastructure.Persistence.ServiceConfiguration
{
    public class EntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        IRequestContext _requestContext;
        public EntitySaveChangesInterceptor(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(
                    eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<IAuditable>> auditableEntries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<IAuditable>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            if (auditableEntries is not null)
            {
                foreach (EntityEntry<IAuditable> auditable in auditableEntries)
                {
                    if (auditable.State == EntityState.Added)
                    {
                        auditable.Entity.CreatedBy = _requestContext?.UserName ?? "Admin";
                        auditable.Entity.CreatedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        auditable.Entity.UpdatedBy = _requestContext?.UserName ?? "Admin";
                        auditable.Entity.UpdatedOn = DateTime.UtcNow;
                    }
                }
            }

            IEnumerable<EntityEntry<ISoftDeletable>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<ISoftDeletable>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDeletable> softDeletable in entries)
            {
                softDeletable.State = EntityState.Modified;
                softDeletable.Entity.IsDeleted = true;
                softDeletable.Entity.DeletedOn = DateTime.UtcNow;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}

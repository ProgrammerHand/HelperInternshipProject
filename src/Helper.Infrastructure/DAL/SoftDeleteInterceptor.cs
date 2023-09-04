using Helper.Core.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Helper.Infrastructure.DAL
{
    internal sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        private readonly IClockCustom _clock;

        public SoftDeleteInterceptor(IClockCustom clock)
        {
            _clock = clock;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry.State is EntityState.Deleted && entry.Entity is ISoftDelete delete)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                    entry.CurrentValues["DeletedAt"] = _clock.Now;
                    foreach (var navigationEntry in entry.Navigations.Where(x => !((IReadOnlyNavigation)x.Metadata).IsOnDependent))
                    {
                        if (navigationEntry is CollectionEntry collectionEntry)
                        {
                            foreach (var dependentEntry in collectionEntry.CurrentValue)
                            {
                                HandleDependent(eventData.Context.Entry(dependentEntry));
                            }
                        }
                        else
                        {
                            var dependentEntry = navigationEntry.CurrentValue;
                            if (dependentEntry != null)
                            {
                                HandleDependent(eventData.Context.Entry(dependentEntry));
                            }
                        }
                    }
                }
                if (entry.State is EntityState.Added && entry.Entity is IDataAudit created) 
                {
                    entry.CurrentValues["CreatedAt"] = _clock.Now;
                    entry.CurrentValues["ModifiedAt"] = _clock.Now;
                }

                if (entry.State is EntityState.Modified && entry.Entity is IDataAudit modified)
                {
                    entry.CurrentValues["ModifiedAt"] = _clock.Now;
                }

            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private void HandleDependent(EntityEntry entry)
        {
            entry.CurrentValues["IsDeleted"] = true;
            entry.CurrentValues["DeletedAt"] = _clock.Now;
        }

        //public override InterceptionResult<int> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result)
        //{
        //    if (eventData.Context is null) return result;

        //    foreach (var entry in eventData.Context.ChangeTracker.Entries())
        //    {
        //        if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete })
        //        {
        //            continue;
        //        }
        //        entry.State = EntityState.Modified;
        //        delete.IsDeleted = true;
        //        delete.DeletedAt = _clock.Now;
        //    }
        //    return result;
        //}
    }
}

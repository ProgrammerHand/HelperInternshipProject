using Helper.Core;
using Helper.Core.Inquiry.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
                var entity = entry.GetType();
                if (entry.State is EntityState.Deleted && entry.Entity is ISoftDelete delete)
                {
                    entry.State = EntityState.Modified;
                    delete.IsDeleted = true;
                    delete.DeletedAt = _clock.Now;
                }

            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
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

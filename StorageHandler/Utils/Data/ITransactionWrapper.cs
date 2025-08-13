using Microsoft.EntityFrameworkCore.Storage;

namespace StorageHandler.Utils.Data;

public interface ITransactionWrapper 
{
    Task<TEntity> Execute<TEntity>(Func<IDbContextTransaction?, Task<TEntity>> func, CancellationToken cancellationToken = new()) where TEntity : class; 
}
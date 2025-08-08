namespace StorageHandler.Utils.Data;

public interface ITransactionWrapper 
{
    Task<TEntity> Execute<TEntity>(Func<Task<TEntity>> func, CancellationToken cancellationToken = new()) where TEntity : class; 
}
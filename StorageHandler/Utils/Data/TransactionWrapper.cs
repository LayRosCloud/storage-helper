namespace StorageHandler.Utils.Data;

public class TransactionWrapper : ITransactionWrapper
{
    private readonly IStorageContext _storage;

    public TransactionWrapper(IStorageContext storage)
    {
        _storage = storage;
    }

    public async Task<TEntity> Execute<TEntity>(Func<Task<TEntity>> func, CancellationToken cancellationToken = new()) where TEntity : class
    {
        var transaction = await _storage.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await func();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
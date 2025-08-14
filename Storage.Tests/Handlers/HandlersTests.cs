using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using StorageHandler.Utils.Data;

namespace Storage.Tests.Handlers;

public abstract class HandlersTests
{
    protected static void SettingDefaultWrapper<TResult>(Mock<ITransactionWrapper> wrapper) where TResult : class
    {
        wrapper
            .Setup(w => w.Execute(
                It.IsAny<Func<IDbContextTransaction?, Task<TResult>>>(),
                It.IsAny<CancellationToken>()))
            .Returns<Func<IDbContextTransaction?, Task<TResult>>, CancellationToken>(
                (func, _) => func(null));
    }
}
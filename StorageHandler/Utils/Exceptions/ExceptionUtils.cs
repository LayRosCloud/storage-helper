namespace StorageHandler.Utils.Exceptions;

public static class ExceptionUtils
{
    public static NotFoundException GetNotFoundException(string message)
    {
        return new NotFoundException(message);
    }

    public static NotFoundException GetNotFoundException(string objectName, object id)
    {
        return new NotFoundException($"{objectName} with id {id} is not found!");
    }
}
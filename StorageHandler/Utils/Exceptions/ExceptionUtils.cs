namespace StorageHandler.Utils.Exceptions;

public static class ExceptionUtils
{
    public static NotFoundException GetNotFoundException(string message)
    {
        return new NotFoundException(message);
    }
}
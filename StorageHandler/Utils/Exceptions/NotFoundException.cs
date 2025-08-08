namespace StorageHandler.Utils.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : this("Object is not found") {}
    public NotFoundException(string message) : base(message) {}
}
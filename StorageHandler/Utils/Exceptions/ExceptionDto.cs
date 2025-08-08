namespace StorageHandler.Utils.Exceptions;

public class ExceptionDto
{
    public ExceptionDto(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }


    public int StatusCode { get; set; }
    public string Message { get; set; }
}
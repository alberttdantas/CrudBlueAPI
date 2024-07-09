namespace CrudBlue.Application.Responses;

public class ServiceResponse<T>
{
    public ServiceResponse(T? data, string message, bool success)
    {
        Data = data;
        Message = message;
        Success = success;
    }

    public T? Data { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}
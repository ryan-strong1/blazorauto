namespace Auto.API.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T data, string message = "")
        {
            Message = message;
            Data = data;
        }

        public ApiResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }
    }
}
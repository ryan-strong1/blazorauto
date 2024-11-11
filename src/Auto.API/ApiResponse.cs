namespace Auto.API.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public Dictionary<string, List<string>> ValidationErrors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
            ValidationErrors = new Dictionary<string, List<string>>();
        }

        public ApiResponse(T data, bool success = true, string message = "")
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = new List<string>();
            ValidationErrors = new Dictionary<string, List<string>>();
        }

        public ApiResponse(string message, bool success = false)
        {
            Success = success;
            Message = message;
            Errors = new List<string>();
            ValidationErrors = new Dictionary<string, List<string>>();
        }

        public ApiResponse(string message, List<string> errors)
        {
            Success = false;
            Message = message;
            Errors = errors;
            ValidationErrors = new Dictionary<string, List<string>>();
        }
    }
}
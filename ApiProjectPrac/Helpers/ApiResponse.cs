namespace ApiProjectPrac.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Request successful";
        public T Data { get; set; }
        public string Error { get; set; }

        public ApiResponse() { }
        public ApiResponse(bool success, string message, T data, string error)
        {
            Success = success;
            Message = message;
            Data = data;
            Error = error;
        }
    }
}

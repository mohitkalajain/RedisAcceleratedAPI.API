namespace RedisAcceleratedAPI.API.Helper
{
    public class ResponseVM
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic Data { get; set; }

        public ResponseVM(int statusCode, string message, dynamic data)
        {
            StatusCode = statusCode;
            Success = true;
            Message = message;
            Data = data;
        }

        public ResponseVM(int statusCode, string message)
        {
            StatusCode = statusCode;
            Success = false;
            Message = message;
            Data = string.Empty;
        }
    }

}

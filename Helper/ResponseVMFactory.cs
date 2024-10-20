namespace RedisAcceleratedAPI.API.Helper
{
    public class ResponseVMFactory
    {
        public static ResponseVM Success(dynamic data, string message = "Request succeeded")
        {
            return new ResponseVM(200, message, data);
        }

        public static ResponseVM Error(string message, int statusCode)
        {
            return new ResponseVM(statusCode, message);
        }
        public static ResponseVM NotFound(string message, int statusCode)
        {
            return new ResponseVM(statusCode, message);
        }
        public static ResponseVM BadRequest(string message, int statusCode)
        {
            return new ResponseVM(statusCode, message);
        }
    }
}

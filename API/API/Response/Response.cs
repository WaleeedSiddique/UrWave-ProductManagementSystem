
namespace API.Response
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public Object Errors { get; set; }
        public bool IsSuccess { get; set; } 

        public Response(string message)
        {
            StatusCode = 400;
            Message = message;
            IsSuccess = false;
        }
        public Response(string message ,int code , T Data) : this(message)
        {
            StatusCode = code;
            Message = message;
            IsSuccess = false;
            Result = Data;
        }
        public Response(T Data, string message)
        {
            Result = Data;
            Message = message;
            IsSuccess = true;
            StatusCode = 200;
        }
    }
}

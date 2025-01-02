namespace API.Response
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string TraceIdentifier { get; set; }
        public string Detailed { get; set; }
    }
}

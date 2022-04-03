using System;

namespace board
{
    public class ErrorResponse
    {

        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }
        
        public string Message { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
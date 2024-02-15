using System.ComponentModel.DataAnnotations;

namespace CGI.API.Messages
{
    public class ResponseApi<T>
    {
        [Required] public int Status { get; }
        [Required] public T PayLoad { get; }
        [Required] public bool IsError { get; }
        [Required] public DateTime Timestamp { get; }

        private ResponseApi(T items, int statusCode = 0)
        {
            if (statusCode != 0)
                Status = statusCode;
            PayLoad = items;
            Timestamp = DateTime.Now;
        }

        private ResponseApi(T payload, int statusCode, bool isError = true)
        {
            PayLoad = payload;
            Status = statusCode;
            IsError = isError;
            Timestamp = DateTime.Now;
        }

        public static ResponseApi<T> GetData(T data, int status)
        {
            return new ResponseApi<T>(data, status);
        }

   
    }
}
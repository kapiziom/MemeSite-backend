using System;
using System.Net;

namespace MemeSite.Domain.Models
{
    public class MemeSiteException : Exception
    {
        // Holds Http status code: 404 NotFound, 400 BadRequest, ...
        public int StatusCode { get; }
        public string MessageDetail { get; set; }
        public object Value { get; set; }
        public object Result { get; set; }

        public MemeSiteException(HttpStatusCode statusCode, string message = null) : base(message)
        {
            StatusCode = (int)statusCode;
        }

        public MemeSiteException(HttpStatusCode statusCode, string message = null, object result = null) : base(message)
        {
            StatusCode = (int)statusCode;
            Result = result;
        }


    }
}

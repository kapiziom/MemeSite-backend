using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Middleware
{
    public class ExceptionMessage
    {
        public string Message { get; set; }

        public ExceptionMessage() { }

        public ExceptionMessage(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { message = new string(Message) });
        }
    }
}

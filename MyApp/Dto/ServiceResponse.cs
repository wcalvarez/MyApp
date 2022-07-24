using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Dto
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}

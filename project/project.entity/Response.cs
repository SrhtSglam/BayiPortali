using System.Collections.Generic;

namespace project.entity{
    public class Response<T>{
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
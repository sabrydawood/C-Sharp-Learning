using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

    public class Response<T>
    {
        public required string Message { get; set; }
        public required string? Error { get; set; }
        public string? Method { get; set; }
        public string? Path { get; set; }
        public DateTime? Date { get; set; }
        public T? Data { get; set; }


    }


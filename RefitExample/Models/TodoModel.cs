using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RefitExample.Models
{

    public class TodoResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }

    public class TodoDto
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
    }

    public class Todo
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("todo")]
        public string title { get; set; }

        [JsonPropertyName("completed")]
        public bool isDone { get; set; }

        [JsonPropertyName("userId")]
        public int userId { get; set; }
    }

}

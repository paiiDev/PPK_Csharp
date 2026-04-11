using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DIExample.Models.Domain
{
    public class DomainTodoDto
    {
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
}

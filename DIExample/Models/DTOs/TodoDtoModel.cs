using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIExample.Models.DTOs
{
    public class TodoDtoModel
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

    }
}

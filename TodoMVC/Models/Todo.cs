namespace TodoMVC.Models
{


    public class BaseResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }

    public class Todo
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
    }

}

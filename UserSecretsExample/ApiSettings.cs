using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSecretsExample
{
    public class ApiSettings
    {
        [Required]
        [Url]
        public string BaseUrl { get; set; }
        [Required]
        public string ApiKey { get; set; }
    }
}

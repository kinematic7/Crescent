using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}

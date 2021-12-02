using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.Models
{
    public class ResponseMessageBM<T>
    {
        public T Response { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}

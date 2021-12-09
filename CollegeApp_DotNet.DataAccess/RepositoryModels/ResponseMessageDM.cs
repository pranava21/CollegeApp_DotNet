using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.DataAccess.RepositoryModels
{
    public class ResponseMessageDM<T>
    {
        public T Response { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}

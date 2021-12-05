using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp_DotNet.BusinessDomain.Models
{
    public class DepartmentDetailsBM
    {
        public Guid DepartmentUid { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentImageUrl { get; set; }
    }
}

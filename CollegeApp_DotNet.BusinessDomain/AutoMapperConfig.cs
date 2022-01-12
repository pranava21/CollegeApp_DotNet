using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CollegeApp_DotNet.BusinessDomain.Models;
using CollegeApp_DotNet.DataAccess.RepositoryModels;

namespace CollegeApp_DotNet.BusinessDomain
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddStudentDetailsBM, AddStudentDetailsDM>().ReverseMap();
            CreateMap<DepartmentDetailsBM, DepartmentDetailsDM>().ReverseMap();
            CreateMap<AttendanceModelBM, AttendanceModelDM>().ReverseMap();
            CreateMap<List<AttendanceModelBM>, List<AttendanceModelDM>>().ReverseMap();
            CreateMap<FacultyDetailsDM, FacultyDetailsBM>().ReverseMap();
        }
    }
}

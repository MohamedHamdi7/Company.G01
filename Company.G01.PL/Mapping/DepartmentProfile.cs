using AutoMapper;
using Company.G01.DAL.Models;
using Company.G01.PL.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.G01.PL.Mapping
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentdto, Department>();
            CreateMap<Department, CreateDepartmentdto>();
        }
    }
}

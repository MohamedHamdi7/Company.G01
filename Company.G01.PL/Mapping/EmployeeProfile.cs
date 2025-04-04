using AutoMapper;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;

namespace Company.G01.PL.Mapping
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {              //from          :     to
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(d=>d.Name, o=>o.MapFrom(s=>s.EmpName));
         // CreateMap<CreateEmployeeDto, Employee>().ReverseMap; ==  CreateMap<Employee, CreateEmployeeDto>();
            CreateMap<Employee, CreateEmployeeDto>();
        }

    }
}

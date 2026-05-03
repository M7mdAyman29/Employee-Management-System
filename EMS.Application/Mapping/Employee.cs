using AutoMapper;
using EMS.Application.DTO.Empolyee;
using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Mapping.Employees
{
    public partial class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<AddEmployeeDTO, Employee>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            
            CreateMap<Employee,EmployeeDTO>()
                 .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom( src => src.Department.Name))
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));


            CreateMap<UpdateEmployeeDTO, Employee>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}

using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.DAL.Models;
using AutoMapper;

namespace ASP.NET.Assignment.PL.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDTO, Employee>().ReverseMap();
        }
    }
}

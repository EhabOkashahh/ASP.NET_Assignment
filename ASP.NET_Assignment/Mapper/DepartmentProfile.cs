using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.DAL.Models;
using AutoMapper;

namespace ASP.NET.Assignment.PL.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
        }
    }
}

using API.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FirstName, mo => mo.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, mo => mo.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, mo => mo.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, mo => mo.MapFrom(src => src.Email));
        }
    }
}

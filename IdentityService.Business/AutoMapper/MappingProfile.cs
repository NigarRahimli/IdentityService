using AutoMapper;
using IdentityService.Core.Entities.Concrete;
using IdentityService.Entities.DTOs.UserDTOs;
using IdentityService.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.MessageHeaders;

namespace IdentityService.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, AppUser>().ReverseMap();
     
       


        }
    }
}

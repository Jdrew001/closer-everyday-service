using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CED.Models.Core;
using CED.Models.DTO;

namespace CED.Profiles
{
    public class AuthCodeProfile : Profile
    {
        public AuthCodeProfile()
        {
            CreateMap<AuthCode, AuthCodeDTO>().ReverseMap();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;
using complainSystem.models.Users;


namespace complainSystem
{
    public class MappingDtos : Profile
    {

        public MappingDtos()
        {
            CreateMap<Complain, AddComplainDto>();
            CreateMap<Complain, UpdateComplainDto>();
            CreateMap<AddComplainDto, Complain>();
            CreateMap<UserRegister, User>();
            CreateMap<User, UserRegister>();


            // CreateMap<UpdateCharacterDto, Character>();
        }

    }
}
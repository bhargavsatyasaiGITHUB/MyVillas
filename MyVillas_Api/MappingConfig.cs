using AutoMapper;
using MyVillas_Api.Models;
using MyVillas_Api.Models.Dto;

namespace MyVillas_Api
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa,VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa,VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}

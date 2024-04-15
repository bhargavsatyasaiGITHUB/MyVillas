using AutoMapper;
using MyVillas_Web.Models;
using MyVillas_Web.Models.Dto;

namespace MyVillas_Web
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
           

            CreateMap<VillaDto,VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDTO>().ReverseMap();



            
           

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}

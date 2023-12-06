
using Microsoft.AspNetCore.Http.HttpResults;

namespace MagicVilla.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<VillaDTO, VillaAPI>();
            CreateMap<VillaAPI,VillaDTO >();
            CreateMap<VillaNumberDTO, VillaNumber>();
            CreateMap<VillaNumber, VillaNumberDTO>();
            CreateMap<VillaNumberCreateDTO, VillaNumber>();
            CreateMap<VillaNumber, VillaNumberCreateDTO>();
            CreateMap<VillaNumberUpdateDTO, VillaNumber>();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>();
        }
        
    }
}

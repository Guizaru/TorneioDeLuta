using API_Torneio.DTO.TorneioDTO;
using API_Torneio.Models.Entities;
using AutoMapper;

namespace API_Torneio.MappingProfiles
{
    public class TorneioProfile : Profile
    {
        public TorneioProfile()
        {
            CreateMap<DtoCreateTorneio, Torneio>();

            CreateMap<DtoEditTorneio, Torneio>();

            CreateMap<Torneio, DtoTorneio>();
        }
    }
}

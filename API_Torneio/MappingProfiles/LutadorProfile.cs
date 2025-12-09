using API_Torneio.DTO.LutadorDTO;
using API_Torneio.Models.Entities;
using AutoMapper;

namespace API_Torneio.MappingProfiles
{
    public class LutadorProfile : Profile
    {
        public LutadorProfile()
        {
            CreateMap<DtoCreateLutador, Lutador>();

            CreateMap<DtoEditLutador, Lutador>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember, destMember) =>
                    {
                        if (srcMember == null) return false;

                        if (srcMember is int intValue && intValue == 0) return false;

                        return true;
                    }));

            CreateMap<Lutador, DtoLutador>();
        }   
    }
}

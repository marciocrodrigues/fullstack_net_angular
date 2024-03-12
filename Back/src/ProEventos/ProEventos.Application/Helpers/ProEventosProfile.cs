using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Entities;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(d => d.DataEvento, o => o.MapFrom((s, d) =>
                {
                    if (s.DataEvento.HasValue)
                        return s.DataEvento.Value.ToString("yyyy-MM-dd HH:mm:ss");

                    return null;
                }))
                .ForMember(d => d.ImageURL, o => o.MapFrom(s => "https://picsum.photos/200/300"))
                .ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
        }
    }
}

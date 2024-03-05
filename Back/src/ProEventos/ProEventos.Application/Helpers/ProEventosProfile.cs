﻿using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Entities;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>();
        }
    }
}

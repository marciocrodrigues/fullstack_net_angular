using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Application.Implementations
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IMapper _mapper;

        public EventoService(IEventoPersistence eventoPersistence, IMapper mapper)
        {
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var eventoEntity = _mapper.Map<Evento>(model);
                
                _eventoPersistence.Add(eventoEntity);

                if (await _eventoPersistence.SaveChangesAsync())
                {
                    var retorno = await _eventoPersistence
                        .GetWithFilterWithoutAsNoTracking(x => x.Id == eventoEntity.Id)
                        .FirstOrDefaultAsync();

                    return _mapper.Map<EventoDto>(retorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar adicionar novo Evento: {ex.Message}");
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var eventoEntity = await _eventoPersistence.GetWithFilterFull(x => x.Id == eventoId).FirstOrDefaultAsync();

                if (eventoEntity is null) return null;

                _mapper.Map(model, eventoEntity);

                _eventoPersistence.Update(eventoEntity);

                if (await _eventoPersistence.SaveChangesAsync())
                {
                    var retorno = await _eventoPersistence
                        .GetWithFilterWithoutAsNoTracking(x => x.Id == eventoEntity.Id)
                        .FirstOrDefaultAsync();

                    return _mapper.Map<EventoDto>(retorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar alterar Evento: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.GetWithFilterFull(x => x.Id == eventoId).FirstOrDefaultAsync();

                if (evento is null) throw new Exception("Evento para delete não foi encontrado");

                _eventoPersistence.Delete(evento);
                return await _eventoPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar deletar Evento: {ex.Message}");
            }
        }

        public async Task<EventoDto> BuscarEventoPorId(int eventoId, bool incluirPalestrantes = false)
        {
            if (eventoId == 0) throw new ArgumentException("Identificador do evento é obrigatorio");

            var queryEvento = _eventoPersistence.GetWithFilterWithoutAsNoTracking(x => x.Id == eventoId);

            if (incluirPalestrantes)
                queryEvento.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            var retorno = await queryEvento.FirstOrDefaultAsync();

            return _mapper.Map<EventoDto>(retorno);
        }

        public async Task<IEnumerable<EventoDto>> BuscarEventosPorTema(string tema, bool incluirPalestrantes = false)
        {
            if (string.IsNullOrWhiteSpace(tema)) throw new ArgumentException("Tema no evento é obrigatorio");

            var queryEvento = _eventoPersistence
                .GetWithFilterWithoutAsNoTracking(x => x.Tema.ToLower()
                    .Contains(tema.ToLower()));

            if (incluirPalestrantes)
                queryEvento.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);

            var retorno = await queryEvento.ToListAsync();

            return _mapper.Map<List<EventoDto>>(retorno);

        }

        public async Task<IEnumerable<EventoDto>> BuscarTodosEventos(bool incluirPalestrantes = false)
        {
            var queryEvento = _eventoPersistence.GetWithFilterWithoutAsNoTracking(x => x.Id != 0);

            if (incluirPalestrantes)
                queryEvento.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);

            var retorno =  await queryEvento.ToListAsync();

            return _mapper.Map<List<EventoDto>>(retorno);
        }
    }
}

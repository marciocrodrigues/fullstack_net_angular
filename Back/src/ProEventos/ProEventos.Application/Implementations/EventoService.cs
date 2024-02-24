using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Implementations
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersistence _eventoPersistence;

        public EventoService(IEventoPersistence eventoPersistence)
        {
            _eventoPersistence = eventoPersistence;
        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _eventoPersistence.Add(model);

                if (await _eventoPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence
                        .GetWithFilterWithoutAsNoTracking(x => x.Id == model.Id)
                        .FirstOrDefaultAsync();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar adicionar novo Evento: {ex.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetWithFilter(x => x.Id == eventoId).FirstOrDefaultAsync();
                if (evento == null) return null;

                model.Id = evento.Id;

                _eventoPersistence .Update(model);

                if (await _eventoPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence
                        .GetWithFilterWithoutAsNoTracking(x => x.Id == model.Id)
                        .FirstOrDefaultAsync();
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
                var evento = await _eventoPersistence.GetWithFilter(x => x.Id == eventoId).FirstOrDefaultAsync();
                if (evento == null) throw new Exception("Evento para delete não foi encontrado");

                _eventoPersistence.Delete(evento);
                return await _eventoPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar deletar Evento: {ex.Message}");
            }
        }

        public async Task<Evento> BuscarEventoPorId(int eventoId, bool incluirPalestrantes = false)
        {
            var queryEvento = _eventoPersistence.GetWithFilterWithoutAsNoTracking(x => x.Id == eventoId);

            if (incluirPalestrantes)
                queryEvento.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            return await queryEvento.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Evento>> BuscarEventosPorTema(string tema, bool incluirPalestrantes = false)
        {
            var queryEvento = _eventoPersistence.GetWithFilterWithoutAsNoTracking(x => x.Tema.Contains(tema));

            if (incluirPalestrantes)
                queryEvento.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            return await queryEvento.ToListAsync();

        }

        public async Task<IEnumerable<Evento>> BuscarTodosEventos(bool incluirPalestrantes = false)
        {
            var queryEvento = _eventoPersistence.GetWithFilterWithoutAsNoTracking(x => x.Id != null));

            if (incluirPalestrantes)
                queryEvento.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            return await queryEvento.ToListAsync();
        }
    }
}

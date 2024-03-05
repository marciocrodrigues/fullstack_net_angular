using ProEventos.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
        Task<bool> Delete(int eventoId);
        Task<IEnumerable<EventoDto>> BuscarTodosEventos(bool incluirPalestrantes = false);
        Task<IEnumerable<EventoDto>> BuscarEventosPorTema(string tema, bool incluirPalestrantes = false);
        Task<EventoDto> BuscarEventoPorId(int eventoId, bool incluirPalestrantes = false);
    }
}

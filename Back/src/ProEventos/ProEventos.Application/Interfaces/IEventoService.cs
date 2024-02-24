using ProEventos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEvento(int eventoId, Evento model);
        Task<bool> Delete(int eventoId);
        Task<IEnumerable<Evento>> BuscarTodosEventos(bool incluirPalestrantes = false);
        Task<IEnumerable<Evento>> BuscarEventosPorTema(string tema, bool incluirPalestrantes = false);
        Task<Evento> BuscarEventoPorId(int eventoId, bool incluirPalestrantes = false);
    }
}

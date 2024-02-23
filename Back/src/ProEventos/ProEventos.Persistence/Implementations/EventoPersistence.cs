using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.ProEventos;

namespace ProEventos.Persistence.Implementations
{
    public class EventoPersistence : ProEventosPersistence<Evento>, IEventoPersistence
    {
        private readonly DbSet<Evento> dbEvento;
        public EventoPersistence(ProEventosContext context) : base(context) => dbEvento = context.Set<Evento>();
    }
}

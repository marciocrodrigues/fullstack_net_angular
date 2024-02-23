using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.ProEventos;

namespace ProEventos.Persistence.Implementations
{
    public class PalestrantePersistence : ProEventosPersistence<Palestrante>, IPalestrantePersistence
    {
        private readonly DbSet<Palestrante> dbPalestrante;
        public PalestrantePersistence(ProEventosContext context) : base(context) => dbPalestrante = context.Set<Palestrante>();
    }
}

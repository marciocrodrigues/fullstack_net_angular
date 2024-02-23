using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.ProEventos;

namespace ProEventos.Persistence.Implementations
{
    public class PalestranteEventoPersistence : ProEventosPersistence<PalestranteEvento>, IPalestranteEventoPersistence
    {
        private readonly DbSet<PalestranteEvento> dbPalestranteEvento;

        public PalestranteEventoPersistence(ProEventosContext context) : base(context) => dbPalestranteEvento = context.Set<PalestranteEvento>();
    }
}

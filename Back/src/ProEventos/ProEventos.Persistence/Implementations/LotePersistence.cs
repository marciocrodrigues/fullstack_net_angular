using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.ProEventos;

namespace ProEventos.Persistence.Implementations
{
    public class LotePersistence : ProEventosPersistence<Lote>, ILotePersistence
    {
        private readonly DbSet<Lote> dbLote;
        public LotePersistence(ProEventosContext context) : base(context) => dbLote = context.Set<Lote>();
    }
}

using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.ProEventos;

namespace ProEventos.Persistence.Implementations
{
    public class RedeSocialPersistence : ProEventosPersistence<RedeSocial>, IRedeSocialPersistence
    {
        private readonly DbSet<RedeSocial> dbRedeSocial;

        public RedeSocialPersistence(ProEventosContext context) : base(context) => dbRedeSocial = context.Set<RedeSocial>();
    }
}

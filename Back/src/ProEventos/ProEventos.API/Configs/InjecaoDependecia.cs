using Microsoft.Extensions.DependencyInjection;
using ProEventos.Application.Implementations;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Implementations;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.API.Configs
{
    public static class InjecaoDependecia
    {
        public static void InjetarDependencias(this IServiceCollection service)
        {
            #region Persistences
            service.AddScoped<IEventoPersistence, EventoPersistence>();
            service.AddScoped<IPalestrantePersistence, PalestrantePersistence>();
            service.AddScoped<ILotePersistence, LotePersistence>();
            service.AddScoped<IRedeSocialPersistence, RedeSocialPersistence>();
            service.AddScoped<IPalestranteEventoPersistence, PalestranteEventoPersistence>();
            #endregion

            #region Services
            service.AddScoped<IEventoService, EventoService>();
            #endregion
        }
    }
}

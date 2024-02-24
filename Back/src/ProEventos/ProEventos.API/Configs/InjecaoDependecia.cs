using Microsoft.Extensions.DependencyInjection;
using ProEventos.Persistence.Implementations;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.API.Configs
{
    public static class InjecaoDependecia
    {
        public static void InjetarDependencias(this IServiceCollection service)
        {
            service.AddScoped<IEventoPersistence, EventoPersistence>();
            service.AddScoped<IPalestrantePersistence, PalestrantePersistence>();
            service.AddScoped<ILotePersistence, LotePersistence>();
            service.AddScoped<IRedeSocialPersistence, RedeSocialPersistence>();
            service.AddScoped<IPalestranteEventoPersistence, PalestranteEventoPersistence>();
        }
    }
}

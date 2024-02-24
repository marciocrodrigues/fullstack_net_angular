using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private IEventoPersistence _eventoPersistence;

        public EventosController(IEventoPersistence eventoPersistence)
        {
            _eventoPersistence = eventoPersistence;
        }

        [HttpGet]
        public async Task<IEnumerable<Evento>> Get()
        {
            return await _eventoPersistence.GetWithFilter(x => x.Id != null).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Evento>> GetById(int id)
        {
            return await _eventoPersistence.GetWithFilter(x => x.Id == id).ToListAsync();
        }

        [HttpPost]
        public string Post()
        {
            return "Examplo de Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put com id {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete com id {id}";
        }
    }
}


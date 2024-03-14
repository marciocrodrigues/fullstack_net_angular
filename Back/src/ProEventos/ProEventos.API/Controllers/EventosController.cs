using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _eventoService.BuscarTodosEventos(true);

                if (result is null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na requisição {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _eventoService.BuscarEventoPorId(id, true);

                if (result is null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na requisição {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var result = await _eventoService.BuscarEventosPorTema(tema, true);

                if (result is null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na requisição {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventoDto model)
        {
            try
            {
                var result = await _eventoService.AddEventos(model);

                if (result is null) return BadRequest("Não foi possivel incluir novo evento");

                return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na requisição {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EventoDto model)
        {
            try
            {
                var result = await _eventoService.UpdateEvento(id, model);

                if (result is null) return BadRequest("Não foi possivel alterar o evento");

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na requisição {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _eventoService.Delete(id);

                if (deleted) return Ok(new { message = "Deletado" });

                return BadRequest("Não foi possivel excluir o evento");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na requisição {ex.Message}");
            }
        }
    }
}


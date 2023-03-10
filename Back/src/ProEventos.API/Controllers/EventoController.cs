using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Contexts;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _service;

        public EventoController(IEventoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            try
            {
                var eventos = await _service.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os eventos. Erro {ex.Message}");
        
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            try
            {
                var evento = await _service.GetEventosByIdAsyc(id, true);
                if (evento == null) return NotFound("Nenhum evento encontrado");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento por ID. Erro {ex.Message}");
        
            }
            
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            
            try
            {
                var evento = await _service.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Nenhum evento encontrado");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento por tema. Erro {ex.Message}");
        
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model){
            try
            {
                var evento = await _service.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao adicionar o evento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento por tema. Erro {ex.Message}");
        
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Evento model){
            try
            {
                var evento = await _service.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao atualizar o evento");

                return NoContent();
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento por tema. Erro {ex.Message}");
        
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            try
            {
                if (!await _service.DeleteEvento(id)) return BadRequest("Erro ao deletar o evento");

                return Ok("Evento deletado");
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento por tema. Erro {ex.Message}");
        
            }
        }
    }
}

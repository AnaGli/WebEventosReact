using System.Collections.Generic;
using Eventos.BLL;
using Eventos.DL.API.Request;
using Eventos.DL.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : GlobalController {
        public AdminController(EventoService eventoService) {
            EventoService = eventoService;
        }

        // Criar EventoService
        [HttpPost("criar")]
        public ActionResult<EventoResponseModel> CadastrarEvento(EventoCreateRequest eventoRequest) {
            if (ModelState.IsValid) {
                var retorno = EventoService.CadastrarEvento(eventoRequest);

                if (retorno != null) {
                    return Ok(retorno);
                }

                ModelState.AddModelError("eventoRequest", "Tentativa de criar EventoService com campos inválidos.");
                return BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }


        // cancelar um EventoService
        [HttpPost("remover")]
        public ActionResult<List<EventoResponseModel>> GetCancelarEvento(int idEvento) {
            EventoResponseModel eventoResult = EventoService.CancelarEvento(idEvento);

            if (eventoResult != null) {
                return Ok(true);
            }

            return BadRequest("Evento com id incorreto.");
        }

        [HttpGet("listarCategorias")]
        public ActionResult<List<CategoriaResponseModelWithId>> GetListaCategorias() {
            List<CategoriaResponseModelWithId> categorias = EventoService.ListarCategorias();

            if (categorias != null) {
                return Ok(categorias);
            }

            return StatusCode(500);
        }

        // Listar participantes de um Evento
        [HttpGet("participantes/{idEvento}")]
        public ActionResult<List<EventoResponseModel>> GetListarParticipantes(int idEvento) {
            List<ParticipacaoResponseModel> participantes = EventoService.ListarParticipantes(idEvento);

            if (participantes != null) {
                return Ok(participantes);
            }

            return NoContent();
        }


        // Iniciar um Evento
        [HttpPost("iniciar")]
        public ActionResult<EventoResponseModel> PostIniciarEvento(int idEvento) {
            EventoResponseModel response = EventoService.IniciarEvento(idEvento);
            if (response != null) {
                return Ok(response);
            }

            return BadRequest("Evento com id incorreto.");
        }

        // Concluir um Evento
        [HttpPost("concluir")]
        public ActionResult<List<EventoResponseModel>> PostConcluirEvento(int idEvento) {
            EventoResponseModel response = EventoService.ConcluirEvento(idEvento);

            if (response != null) {
                return Ok(response);
            }

            return BadRequest("Evento com id incorreto.");
        }
    }
}
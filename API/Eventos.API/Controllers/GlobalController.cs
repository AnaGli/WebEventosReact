using System;
using System.Collections.Generic;
using Eventos.BLL;
using Eventos.DL.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.API.Controllers {
    public abstract class GlobalController : ControllerBase {
        
        public EventoService EventoService;

        // Listar Eventos
        [HttpGet("listar")]
        public ActionResult<List<EventoResponseModel>> GetListarEventos() {
            List<EventoResponseModel> response = EventoService.ListarEventos();
            if (response != null) {
                return Ok(response);
            }

            return NoContent();
        }
       

        // Listar somente os eventos de determinada categoria
        [HttpGet("eventos/categoria/{idCategoriaEvento}")]
        public ActionResult<List<EventoResponseModel>> GetListarEventosPorCategoria(int idCategoriaEvento) {
            var response = EventoService.ListarEventosPorCategoria(idCategoriaEvento);
            if (response != null) {
                return Ok(response);
            }

            return BadRequest();
        }

        // Listar somente os eventos de determinada data
        [HttpGet("eventos/{dataHoraInicio}")]
        public ActionResult<List<EventoResponseModel>> GetListarEventosPorDataDoEvento(DateTime dataHoraInicio) {
            var response = EventoService.ListarEventosPorDataDoEvento(dataHoraInicio);
            if (response != null) {
                return Ok(response);
            }

            ModelState.AddModelError("DataHoraInicio", "Data de início do evento não pode ser hoje.");
            return BadRequest(ModelState);
        }


        // Exibir detalhes de um evento específico
        [HttpGet("detalhes/{idEvento}")]
        public ActionResult<List<EventoResponseModel>> GetDetalhesEvento(int idEvento) {
            EventoResponseModel response = EventoService.DetalhesEvento(idEvento);
            if (response != null) {
                return Ok(response);
            }

            return BadRequest("Evento com id inválido.");
        }
    }
}
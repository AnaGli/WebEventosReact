using Eventos.BLL;
using Eventos.DL.API.Request;
using Eventos.DL.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : GlobalController {
        private ParticipacaoService ParticipacaoService;

        public UsuarioController(ParticipacaoService _participacaoService, EventoService eventoService) {
            ParticipacaoService = _participacaoService;
            EventoService = eventoService;
        }


        // Criar ParticipaçãoService
        [HttpPost("criar")]
        public ActionResult<ParticipacaoResponseModel> CadastrarParticipante(
            ParticipacaoCreateRequest ParticipacaoRequest) {
            if (ModelState.IsValid) {
                var retorno = ParticipacaoService.CadastrarParticipacao(ParticipacaoRequest);

                if (retorno != null) {
                    return Ok(retorno);
                }

                ModelState.AddModelError("ParticipacaoCreateRequest",
                    "Tentativa de criar Participante com campos inválidos.");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("avaliar")]
        public ActionResult<ParticipacaoResponseModel> AvaliarEvento(ParticipacaoAvaliarRequest participacaoAvaliar) {
            if (ModelState.IsValid) {
                ParticipacaoResponseModel response = ParticipacaoService.Avaliar(participacaoAvaliar);

                if (response != null) {
                    ModelState.AddModelError("ParticipacaoReviewRequest",
                        "Impossivel avaliar evento. Possível erro com 'LoginParticipante', 'Nota' ou 'Comentário'.");
                    return Ok(response);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPost("presenca")]
        public ActionResult<ListaParticipacaoResponseModel> PostAlterarPresenca(int IdParticipacao) {
            ListaParticipacaoResponseModel response = ParticipacaoService.AlterarPresenca(IdParticipacao);

            if (response != null) {
                return Ok(response);
            }

            return BadRequest("Id participação incorreto");
        }
    }
}
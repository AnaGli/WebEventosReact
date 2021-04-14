using Eventos.DAL.EntityDBFirst;
using Eventos.DL.API.Request;
using Eventos.DL.API.Response;

namespace Eventos.BLL {
    public class ParticipacaoService {
        private RepositorioParticipacao _repositorioParticipacao;
        private RepositorioEventos _repositorioEventos;

        public ParticipacaoService(RepositorioParticipacao repositorioParticipacao,
            RepositorioEventos repositorioEventos) {
            _repositorioParticipacao = repositorioParticipacao;
            _repositorioEventos = repositorioEventos;
        }

        public ParticipacaoResponseModel CadastrarParticipacao(ParticipacaoCreateRequest participacaoRequest) {
            Participacao participacao = new Participacao() {
                LoginParticipante = participacaoRequest.LoginParticipante,
                IdEvento = participacaoRequest.IdEvento,
            };

            Evento possivelEvento = _repositorioEventos.DetalhesEvento(participacaoRequest.IdEvento);
            bool ehPossivelSeCadastrarNoEvento =
                this.ehPossivelSeCadastrarNoEvento(possivelEvento, participacaoRequest);

            if (ehPossivelSeCadastrarNoEvento) {
                Participacao participacaoResult =
                    _repositorioParticipacao.CadastrarParticipacao(participacao, possivelEvento);

                if (participacaoResult == null) {
                    return null;
                }

                ParticipacaoResponseModel ParticipacaoResponse = new ParticipacaoResponseModel() {
                    LoginParticipante = participacaoResult.LoginParticipante,
                    FlagPresente = participacao.FlagPresente,
                    Nota = participacao.Nota,
                    Comentario = participacao.Comentario,
                    eventoResponseModel = EventoService.Converter(participacaoResult.IdEventoNavigation)
                };

                return ParticipacaoResponse;
            }

            return null;
        }

        private bool ehPossivelSeCadastrarNoEvento(Evento possivelEvento,
            ParticipacaoCreateRequest participacaoRequest) {
            int eventoAbertoParaInscricoes = 1;
            return possivelEvento != null && possivelEvento.LimiteVagas > 0 &&
                   possivelEvento.IdEventoStatus == eventoAbertoParaInscricoes;
        }


        public ParticipacaoResponseModel Avaliar(ParticipacaoAvaliarRequest participacaoAvaliar) {
            Participacao participacao =
                _repositorioParticipacao.carregarParticipacao(participacaoAvaliar.LoginParticipante);

            bool avaliacaoEhValida = AvaliacaoEhValida(participacaoAvaliar);

            if (participacao != null) {
                if (participantePodeAvaliar(participacao) && avaliacaoEhValida) {
                    participacao.Nota = participacaoAvaliar.Nota;
                    participacao.Comentario = participacaoAvaliar.Comentario;
                    // Não estamos salvando no banco, somente instanciando. ISSO ESTA ERRADO!

                    return EventoService.ConverterParticipantes(participacao);
                }
            }

            return null;
        }


        private bool participantePodeAvaliar(Participacao participacao) {
            Evento eventoDoParticipante = _repositorioEventos.DetalhesEvento(participacao.IdEvento);

            int statusConcluido = 3;
            if (eventoDoParticipante != null) {
                bool eventoTerminou = (eventoDoParticipante.IdEventoStatus == statusConcluido);
                bool pessoaCompareceu = participacao.FlagPresente; // Sempre irá dar false com o código atual.
                if (eventoTerminou && pessoaCompareceu) {
                    return true;
                }
            }

            return false;
        }

        public ListaParticipacaoResponseModel AlterarPresenca(int IdParticipacao) {
            if (IdParticipacao > 0) {
                Participacao participacao = _repositorioParticipacao.AlterarPresenca(IdParticipacao);
                return EventoService.ConverterListaParticipantes(participacao);
            }

            return null;
        }


        private bool AvaliacaoEhValida(ParticipacaoAvaliarRequest participacaoAvaliar) {
            return participacaoAvaliar.Nota >= 0 && participacaoAvaliar.Nota <= 10;
        }
    }
}
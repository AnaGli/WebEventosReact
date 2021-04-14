using System;
using System.Collections.Generic;
using System.Linq;
using Eventos.DAL.EntityDBFirst;
using Eventos.DL.API.Request;
using Eventos.DL.API.Response;

namespace Eventos.BLL {
    public class EventoService {
        private readonly RepositorioEventos _repositorioEventos;

        public EventoService(RepositorioEventos repositorioEventos) {
            this._repositorioEventos = repositorioEventos;
        }

        public EventoResponseModel CadastrarEvento(EventoCreateRequest eventoRequest) {
            bool oEventoEhValido = EventoEhValido(eventoRequest);

            if (oEventoEhValido) {
                int abertoParaInscricoes = 1;

                Evento evento = new Evento() {
                    DataHoraFim = eventoRequest.DataHoraFim,
                    DataHoraInicio = eventoRequest.DataHoraInicio,
                    Descricao = eventoRequest.Descricao,
                    IdCategoriaEvento = eventoRequest.IdCategoriaEvento,
                    Local = eventoRequest.Local,
                    LimiteVagas = eventoRequest.LimiteVagas,
                    Nome = eventoRequest.Nome,
                    IdEventoStatus = abertoParaInscricoes
                };

                Evento eventoResult = _repositorioEventos.CadastrarEvento(evento);

                CategoriaResponseModel categoriaResponse =
                    new CategoriaResponseModel(evento.IdCategoriaEventoNavigation.NomeCategoria);
                StatusEventoResponseModel statusEventoResponse =
                    new StatusEventoResponseModel(evento.IdEventoStatusNavigation.NomeStatus);

                EventoResponseModel eventoResponse = new EventoResponseModel() {
                    CategoriaResponseModel = categoriaResponse,
                    DataHoraFim = eventoResult.DataHoraFim,
                    DataHoraInicio = eventoResult.DataHoraInicio,
                    Descricao = eventoResult.Descricao,
                    LimiteVagas = eventoResult.LimiteVagas,
                    Local = eventoResult.Local,
                    Nome = eventoResult.Nome,
                    StatusEventoResponseModel = statusEventoResponse
                };

                return eventoResponse;
            }

            return null;
        }

        private bool EventoEhValido(EventoCreateRequest eventoRequest) {
            bool naoComecaHoje = eventoRequest.DataHoraInicio.DayOfYear != DateTime.Today.DayOfYear;

            bool comecaETerminaNoMesmoAno = eventoRequest.DataHoraInicio.Year == eventoRequest.DataHoraFim.Year;
            bool comecaETerminaNoMesmoDiaDoAno =
                eventoRequest.DataHoraInicio.DayOfYear == eventoRequest.DataHoraFim.DayOfYear;

            bool temHorarioInicioEFimDiferentes =
                eventoRequest.DataHoraInicio.Hour != eventoRequest.DataHoraFim.Hour;

            bool temIdCategoriaValido = eventoRequest.IdCategoriaEvento > 0;
            bool temLimiteDeVagasValido = eventoRequest.LimiteVagas > 0;

            bool todosOsCamposSaoValidos = comecaETerminaNoMesmoAno && comecaETerminaNoMesmoDiaDoAno && naoComecaHoje &&
                                           temHorarioInicioEFimDiferentes && temIdCategoriaValido && temLimiteDeVagasValido;
            return todosOsCamposSaoValidos;
        }

        public List<EventoResponseModel> ListarEventos() {
            var response = new List<EventoResponseModel>(); // cria uma lista vazia do tipo EventoResponseModel

            List<Evento> listaEventos = _repositorioEventos.ListarTodosEventos(); // cria uma lista do tipo Eventos

            if (listaEventos != null && listaEventos.Any()) // verifica se existe algum elemento nulo ou vazio
            {
                foreach (Evento item in listaEventos
                    ) // cada elemento da lista do tipo evento, vai ser adicionado na lista do
                    // tipo EventoResponseModel, e por isso precisamos criar o método Converter.
                {
                    response.Add(Converter(item));
                }
            }

            return response;
        }

        public static EventoResponseModel Converter(Evento model) {
            if (model == null)
                return null;

            var response = new EventoResponseModel() {
                StatusEventoResponseModel = new StatusEventoResponseModel(model.IdEventoStatusNavigation.NomeStatus),
                IdEvento = model.IdEvento,
                Nome = model.Nome,
                DataHoraInicio = model.DataHoraInicio,
                DataHoraFim = model.DataHoraFim,
                Local = model.Local,
                Descricao = model.Descricao,
                LimiteVagas = model.LimiteVagas,
                CategoriaResponseModel = new CategoriaResponseModel(model.IdCategoriaEventoNavigation.NomeCategoria),
            };

            return response;
        }

        public object ListarEventosPorCategoria(int idCategoriaEvento) {
            var response = new List<EventoResponseModel>();

            List<Evento> listaEventosCategoria = _repositorioEventos.ListarEventosPorCategoria(idCategoriaEvento);
            if (listaEventosCategoria != null && listaEventosCategoria.Any()) {
                foreach (Evento item in listaEventosCategoria) {
                    response.Add(Converter(item));
                }
            }

            return response;
        }

        public object ListarEventosPorDataDoEvento(DateTime dataHoraInicio) {
            var response = new List<EventoResponseModel>();

            List<Evento> listaEventosPorDataDoEvento = _repositorioEventos.ListarEventosPorDataDoEvento(dataHoraInicio);
            if (listaEventosPorDataDoEvento != null && listaEventosPorDataDoEvento.Any()) {
                foreach (Evento item in listaEventosPorDataDoEvento) {
                    response.Add(Converter(item));
                }
            }

            return response;
        }


        public List<ParticipacaoResponseModel> ListarParticipantes(int idEvento) {
            var response = new List<ParticipacaoResponseModel>();

            List<Participacao> listaParticipantes = _repositorioEventos.ListarParticipantes(idEvento);

            if (listaParticipantes != null && listaParticipantes.Any()) {
                foreach (Participacao item in listaParticipantes) {
                    response.Add(ConverterParticipantes(item));
                }
            }

            return response;
        }


        public static ParticipacaoResponseModel ConverterParticipantes(Participacao model) {
            if (model == null)
                return null;


            var response = new ParticipacaoResponseModel() {
                IdParticipacao = model.IdParticipacao,
                LoginParticipante = model.LoginParticipante,
                FlagPresente = model.FlagPresente,
            };

            return response;
        }

        public static ListaParticipacaoResponseModel ConverterListaParticipantes(Participacao model) {
            if (model == null)
                return null;


            var response = new ListaParticipacaoResponseModel() {
                idParticipacao = model.IdParticipacao,

                loginParticipante = model.LoginParticipante,
            };

            return response;
        }


        public EventoResponseModel CancelarEvento(int idEvento) {
            if (idEvento > 0) {
                Evento eventoCancelado = _repositorioEventos.CancelarEvento(idEvento);
                return Converter(eventoCancelado);
            }

            return null;
        }


        public EventoResponseModel IniciarEvento(int idEvento) {
            if (idEvento > 0) {
                Evento eventoIniciado = _repositorioEventos.IniciarEvento(idEvento);

                if (eventoIniciado != null) {
                    return Converter(eventoIniciado);
                }
            }

            return null;
        }

        public EventoResponseModel ConcluirEvento(int idEvento) {
            if (idEvento > 0) {
                Evento eventoConcluido = _repositorioEventos.ConcluirEvento(idEvento);
                if (eventoConcluido != null) {
                    return Converter(eventoConcluido);
                }
            }

            return null;
        }


        public EventoResponseModel DetalhesEvento(int idEvento) {
            if (idEvento > 0) {
                Evento detalheEventos = _repositorioEventos.DetalhesEvento(idEvento);

                if (detalheEventos != null) {
                    return Converter(detalheEventos);
                }
            }

            return null;
        }

        public List<CategoriaResponseModelWithId> ListarCategorias() {
            return _categoriaConverter(_repositorioEventos.ListarCategorias());
        }

        private List<CategoriaResponseModelWithId> _categoriaConverter(List<CategoriaEvento> categoriaEventos) {
            List<CategoriaResponseModelWithId> categorias = new List<CategoriaResponseModelWithId>();
            foreach (CategoriaEvento categoria in categoriaEventos) {
                categorias.Add(new CategoriaResponseModelWithId(categoria.NomeCategoria, categoria.IdCategoriaEvento));
            }

            return categorias;
        }
    }
}
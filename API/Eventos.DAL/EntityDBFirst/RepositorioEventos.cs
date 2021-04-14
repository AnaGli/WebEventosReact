using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eventos.DAL.EntityDBFirst {
    public class RepositorioEventos {
        /*
         *  1.	CadastrarEvento(Evento evento) 
            2.	CancelarEvento(Evento evento) 
            3.	ListarParticipantes(Evento evento) 
            4.	IniciarEvento(Evento evento) 
            5.	AlterarStatusEvento(Evento evento) 
            6.	ConcluirEvento(Evento evento) 
            7.	DetalhesEvento(Evento evento)

         */


        public Evento CadastrarEvento(Evento evento) {
            using (var db = new EVENTOSContext()) {
                IQueryable<StatusEvento> statusEventoQuery = db.StatusEventos.Where(x => x.IdEventoStatus == 1);

                IQueryable<CategoriaEvento> categoriaEventoQuery =
                    db.CategoriaEventos.Where(x => x.IdCategoriaEvento == evento.IdCategoriaEvento);

                evento.IdEventoStatusNavigation = statusEventoQuery.First();
                evento.IdCategoriaEventoNavigation = categoriaEventoQuery.First();

                db.Set<Evento>().Add(evento);
                db.SaveChanges();

                return evento;
            }
        }


        public List<Evento> ListarTodosEventos() {
            using (var db = new EVENTOSContext()) {
                IQueryable<Evento> query = db.Eventos;
                IQueryable<Evento> join = JoinsResponseModel(query);
                return join.ToList();
            }
        }

        public List<Evento> ListarEventosPorCategoria(int idCategoriaEvento) {
            using (var db = new EVENTOSContext()) {
                IQueryable<Evento> query = db.Eventos.Where(x => x.IdCategoriaEvento == idCategoriaEvento);
                IQueryable<Evento> join = JoinsResponseModel(query);
                return join.ToList();
            }
        }


        public static IQueryable<Evento> JoinsResponseModel(IQueryable<Evento> query) {
            using (var db = new EVENTOSContext()) {
                query = query.Include(x => x.IdCategoriaEventoNavigation);
                query = query.Include(x => x.IdEventoStatusNavigation);

                return query;
            }
        }


        public List<Evento> ListarEventosPorDataDoEvento(DateTime dataHoraInicio) {
            using (var db = new EVENTOSContext()) {
                IQueryable<Evento> query = db.Eventos.Where(x =>  x.DataHoraInicio.Year == dataHoraInicio.Year
                                                                  && x.DataHoraInicio.Day == dataHoraInicio.Day
                                                                  && x.DataHoraInicio.Month == dataHoraInicio.Month);
                IQueryable<Evento> join = JoinsResponseModel(query);
                return join.ToList();
            }
        }

        public Evento DetalhesEvento(int idEvento) {
            using (var db = new EVENTOSContext()) {
                IQueryable<Evento> query = db.Eventos.Where(x => x.IdEvento == idEvento);
                IQueryable<Evento> join = JoinsResponseModel(query);
                return join.FirstOrDefault();
            }
        }

        public Evento CancelarEvento(int idEvento) {
            using (var db = new EVENTOSContext()) {
                var eventoASerCancelado = DetalhesEvento(idEvento);
                eventoASerCancelado.IdEventoStatus = 4;
                return Atualizar(eventoASerCancelado);
            }
        }

        public List<Participacao> ListarParticipantes(int idEvento) {
            using (var db = new EVENTOSContext()) {
                IQueryable<Participacao> query = db.Participacaos.Where(x => x.IdEvento == idEvento);
                return query.ToList();
            }
        }

        public Evento IniciarEvento(int idEvento) {
            using (var db = new EVENTOSContext()) {
                var eventoASerIniciado = DetalhesEvento(idEvento);
                eventoASerIniciado.IdEventoStatus = 2;
                return Atualizar(eventoASerIniciado);
            }
        }

        public Evento ConcluirEvento(int idEvento) {
            using (var db = new EVENTOSContext()) {
                var eventoASerConcluido = DetalhesEvento(idEvento);
                eventoASerConcluido.IdEventoStatus = 3;
                return Atualizar(eventoASerConcluido);
            }
        }

        public Evento Atualizar(Evento evento) {
            using (var db = new EVENTOSContext()) {
                db.ChangeTracker.AutoDetectChangesEnabled = false;

                db.Entry(evento).State = EntityState.Modified;

                db.SaveChanges();

                return evento;
            }
        }

        public List<CategoriaEvento> ListarCategorias() {
            using (var db = new EVENTOSContext()) {
                IQueryable<CategoriaEvento> query = db.CategoriaEventos;
                return query.ToList();
            }
        }
    }
}
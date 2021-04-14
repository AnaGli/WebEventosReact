using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eventos.DAL.EntityDBFirst {
    public class RepositorioParticipacao {
        public Participacao CadastrarParticipacao(Participacao participacao, Evento evento) {
            using (var db = new EVENTOSContext()) {

                if (carregarParticipacao(participacao.LoginParticipante) != null) return null;
                
                participacao.IdEvento = evento.IdEvento;

                db.Set<Participacao>().Add(participacao);
                db.SaveChanges();
                return participacao;
            }
        }

        public Participacao carregarParticipacao(string loginParticipante) {
            using (var db = new EVENTOSContext()) {
                IQueryable<Participacao> participacaoQuery =
                    db.Participacaos.Where(x => x.LoginParticipante == loginParticipante);

                participacaoQuery.Include(x => x.IdEventoNavigation);

                return participacaoQuery.FirstOrDefault();
            }
        }

        public Participacao AlterarPresenca(int IdParticipacao) {
            using (var db = new EVENTOSContext()) {
                var alterarPresenca = db.Set<Participacao>().Find(IdParticipacao);
                alterarPresenca.FlagPresente = true;
                return Atualizar(alterarPresenca);
            }
        }

        public Participacao Atualizar(Participacao participacao) {
            using (var db = new EVENTOSContext()) {
                db.ChangeTracker.AutoDetectChangesEnabled = false;

                db.Entry(participacao).State = EntityState.Modified;

                db.SaveChanges();

                return participacao;
            }
        }
    }
}
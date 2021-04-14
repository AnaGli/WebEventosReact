using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.DL.API.Response
{
    public class ParticipacaoResponseModel
    {
        //aqui está trazendo todos os campos do EventoResponseModel, mas quero apenas o nome
        public EventoResponseModel  eventoResponseModel { get; set; }

        public int IdParticipacao { get; set; }
        public string LoginParticipante { get; set; }

        public bool FlagPresente { get; set; } //é necessário? 

        public int? Nota { get; set; } 

        public string Comentario  { get; set; } 

    }
}

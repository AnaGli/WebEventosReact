using System;

namespace Eventos.DL.API.Response {
    public class EventoResponseModel {
        public CategoriaResponseModel CategoriaResponseModel { get; set; }


        public StatusEventoResponseModel StatusEventoResponseModel { get; set; }

        public int IdEvento { get; set; }
        public string Nome { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public string Local { get; set; }

        public string Descricao { get; set; }

        public int LimiteVagas { get; set; } //não acho que seja necéssário 
    }
}
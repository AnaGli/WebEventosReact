using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventos.DL.API.Request {
    public class ParticipacaoAvaliarRequest {
        [Required] [StringLength(255)]
        public string LoginParticipante { get; set; }
        [Required] public int Nota { get; set; }
        [Required] [StringLength(255)] public string Comentario { get; set; }
    }
}
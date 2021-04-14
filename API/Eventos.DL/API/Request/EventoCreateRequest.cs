using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Eventos.DL.API.Request {
    public class EventoCreateRequest {
        [Required] [NotNull] [StringLength(255)] public string Nome { get; set; }
        [Required] [NotNull] public DateTime DataHoraInicio { get; set; }

        [Required] public DateTime DataHoraFim { get; set; }

        [Required] [StringLength(255)] public string Local { get; set; }

        [Required] [StringLength(255)] public string Descricao { get; set; }
        [Required] public int LimiteVagas { get; set; }

        [Required] public int IdCategoriaEvento { get; set; }
    }
}
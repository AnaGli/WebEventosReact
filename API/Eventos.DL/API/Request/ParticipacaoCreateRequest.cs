using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Eventos.DL.API.Request
{
    public class ParticipacaoCreateRequest
    {
        [Required] [NotNull] public int IdEvento { get; set; }

        [Required] [NotNull] [StringLength(255)] public string LoginParticipante { get; set; }
    }
}

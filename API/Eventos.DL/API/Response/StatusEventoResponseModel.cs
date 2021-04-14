using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.DL.API.Response
{
    public class StatusEventoResponseModel
    {
        public StatusEventoResponseModel(string nomeStatus)
        {
            NomeStatus = nomeStatus;
        }

        public string NomeStatus { get; set; }
    }
}

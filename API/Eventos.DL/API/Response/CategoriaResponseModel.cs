using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.DL.API.Response
{
   public class CategoriaResponseModel
    {
        public CategoriaResponseModel(string nome)
        {
            NomeCategoria = nome;
        }

        public string NomeCategoria { get; set; }
    }
}

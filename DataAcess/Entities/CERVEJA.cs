using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAcess.Entities
{
    public partial class CERVEJA
    {
        public CERVEJA()
        {
            PESSOA_CERVEJA = new HashSet<PESSOA_CERVEJA>();
        }

        public int ID { get; set; }
        public int TipoID { get; set; }
        public int MarcaID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Teor { get; set; }
        public string Amargor { get; set; }

        public MARCA Marca { get; set; }
        public TIPO Tipo { get; set; }
        public ICollection<PESSOA_CERVEJA> PESSOA_CERVEJA { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAcess.Entities
{
    public partial class TIPO
    {
        public TIPO()
        {
            CERVEJA = new HashSet<CERVEJA>();
        }

        public int ID { get; set; }
        public int FamiliaID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Origem { get; set; }

        public FAMILIA Familia { get; set; }
        public ICollection<CERVEJA> CERVEJA { get; set; }
    }
}

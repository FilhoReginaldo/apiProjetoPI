using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAcess.Entities
{
    public partial class FAMILIA
    {
        public FAMILIA()
        {
            TIPO = new HashSet<TIPO>();
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ICollection<TIPO> TIPO { get; set; }
    }
}

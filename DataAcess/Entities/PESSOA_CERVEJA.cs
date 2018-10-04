using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAcess.Entities
{
    public partial class PESSOA_CERVEJA
    {
        public int ID { get; set; }
        public int PessoaID { get; set; }
        public int CervejaID { get; set; }

        public CERVEJA Cerveja { get; set; }
        public PESSOA Pessoa { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAccess.Entities
{
    public partial class PESSOA_ENDERECO
    {
        public int ID { get; set; }
        public int PessoaID { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public PESSOA Pessoa { get; set; }
    }
}

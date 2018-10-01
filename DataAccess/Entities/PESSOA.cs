using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAccess.Entities
{
    public partial class PESSOA
    {
        public PESSOA()
        {
            PESSOA_ENDERECO = new HashSet<PESSOA_ENDERECO>();
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }

        public ICollection<PESSOA_ENDERECO> PESSOA_ENDERECO { get; set; }
    }
}

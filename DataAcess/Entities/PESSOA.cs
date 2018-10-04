using System;
using System.Collections.Generic;

namespace Efficacy.Api.DataAcess.Entities
{
    public partial class PESSOA
    {
        public PESSOA()
        {
            PESSOA_CERVEJA = new HashSet<PESSOA_CERVEJA>();
            PESSOA_ENDERECO = new HashSet<PESSOA_ENDERECO>();
        }

        public int ID { get; set; }
        public int TipoPessoaID { get; set; }
        public string RazaoSocial { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cpf_Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public ICollection<PESSOA_CERVEJA> PESSOA_CERVEJA { get; set; }
        public ICollection<PESSOA_ENDERECO> PESSOA_ENDERECO { get; set; }
    }
}

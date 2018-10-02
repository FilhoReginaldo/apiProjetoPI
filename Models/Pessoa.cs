using System;

namespace Efficacy.Api.Models
{
    public class Pessoa
    {
        public int ID {get; set;}
        public string Nome {get; set;}
        public int TipoPessoaID {get;set;}
        public string RazaoSocial{get;set;}
        public string Email {get;set;}
        public string Senha {get;set;}
        public string Cpf_Cnpj {get;set;}
        public string Telefone {get;set;}
        public string Celular {get;set;}
        public DateTime DataNascimento {get; set;}
        public DateTime DataCriacao {get; set;}
        public DateTime DataAlteracao {get;set;}
    }
    
}
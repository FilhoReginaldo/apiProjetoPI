namespace Efficacy.Api.Models
{
    public class PessoaEndereco
    {
        public int ID {get; set;}
        public int PessoaID {get; set;}
        public string Logradouro {get; set;}
        public string CEP {get; set;}
        public string Bairro {get; set;}
        public string Cidade {get; set;}
        public string UF {get; set;}   
    }
}
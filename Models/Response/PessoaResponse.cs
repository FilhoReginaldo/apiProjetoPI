using System.Collections.Generic;

namespace Efficacy.Api.Models.Response
{
    public class PessoaResponse: Pessoa
    {
        public List<PessoaEnderecoResponse> Enderecos {get; set;}

    }
    
}
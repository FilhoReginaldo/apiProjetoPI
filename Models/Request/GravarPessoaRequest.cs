using System.Collections.Generic;

namespace Efficacy.Api.Models.Request
{
    public class GravarPessoaRequest: Pessoa
    {
        public List<PessoaEnderecoRequest> Enderecos {get; set;}
    }
}
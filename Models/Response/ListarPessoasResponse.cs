using System.Collections.Generic;

namespace Efficacy.Api.Models.Response
{
    public class ListarPessoasResponse: BaseResponse
    {
        public List<PessoaResponse> Pessoas {get; set;}
    }
}
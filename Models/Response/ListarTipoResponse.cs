using System.Collections.Generic;

namespace Efficacy.Api.Models.Response
{
    public class ListarTipoResponse: BaseResponse
    {
        public List<TipoResponse> Tipo {get;set;}
    }
}
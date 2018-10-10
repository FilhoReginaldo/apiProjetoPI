using System.Collections.Generic;

namespace Efficacy.Api.Models.Response
{
    public class TipoResponse: TipoCerveja
    {
        public List<FamiliaResponse> Familia {get;set;}
    }
}
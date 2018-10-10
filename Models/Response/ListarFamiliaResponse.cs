using System.Collections.Generic;

namespace Efficacy.Api.Models.Response
{
    public class ListarFamiliaResponse: BaseResponse
    {
        public List<FamiliaResponse> Familia {get;set;}
    }
}
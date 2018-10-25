using Efficacy.Api.Business;
using Efficacy.Api.DataAcess.Entities;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Efficacy.Api.Controllers
{
    /// <summary>
    /// Para trabalhar com os registros de Tipo
    /// </summary>
    public class TipoController: Controller
    {
        DbContextOptions<ProjetoAPIContext> contextOptions = null;

        public TipoController(DbContextOptions<ProjetoAPIContext> options)
        {
            contextOptions = options;
        } 

        /// <summary>
        /// Buscar Pessoas: Para Pessoas no Sistema.
        /// </summary>
        /// <param name="queryOptions">Filtros de Pesquisa</param>
        /// <returns>Retorna o Resultado da Pesquisa</returns>
        [HttpGet]
        [Route("tipo/buscar")]
        public ListarTipoResponse Listartipos([FromQuery] ListarTipoRequest request)
        {
            using(var business = new TipoBussiness(contextOptions))
            {
                return business.ListarTipo(request);
            }
        }

        /// <summary>
        /// Gravar Pessoa: Para gravar uma Pessoa.
        /// </summary>
        /// <param name="GravarTipoRequest">Dados necessários para a gravação do Registro.</param>
        /// <returns>Retorna o Resultado do Processamento e o ID do registro gravado. OBS.: O Processamento é executado com sucesso quando o Sucesso for igual a True.</returns>
        [HttpPost]
        [Route("tipo/gravar")]
        public GravarTipoResponse gravarTipo([FromQuery] GravarTipoRequest request)
        {
            using(var business = new TipoBussiness(contextOptions))
            {
                return business.GravarTipo(request);
            }
        }
 

    }
}
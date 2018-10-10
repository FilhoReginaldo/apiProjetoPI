using Efficacy.Api.Business;
using Efficacy.Api.DataAcess.Entities;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Efficacy.Api.Controllers
{
    public class FamiliaController: Controller
    {
    /// <summary>
    /// Para trabalhar com os registros de Familia
    /// </summary>
         DbContextOptions<ProjetoAPIContext> contextOptions = null;
        public FamiliaController(DbContextOptions<ProjetoAPIContext> options)
        {
            contextOptions = options;
        }

        /// <summary>
        /// Buscar Familia Cerveja: Para Pessoas no Sistema.
        /// </summary>
        /// <returns>Retorna o Resultado da Pesquisa</returns>
        [HttpGet]
        [Route("familia/buscar")]
        public ListarFamiliaResponse ListarPessoas([FromQuery] ListaFamiliaRequest request)
        {
            using(FamiliaBusiness business = new FamiliaBusiness(contextOptions))
            {
                return business.FamiliaBuscar(request);
            }
        }

         /// <summary>
        /// Gravar Pessoa: Para gravar uma Familia.
        /// </summary>
        /// <param name="gravarFamiliaRequest">Dados necessários para a gravação do Registro.</param>
        /// <returns>Retorna o Resultado do Processamento e o ID do registro gravado. OBS.: O Processamento é executado com sucesso quando o Sucesso for igual a True.</returns>
        [HttpPost]
        [Route("falimia/gravar")]
        public GravarFamiliaResponse GravarFamilia([FromBody] GravarFamiliaRequest request)
        {
            using(FamiliaBusiness business = new FamiliaBusiness(contextOptions))
            {
                return business.GravarFamilia(request);
            }
        }
    }
}
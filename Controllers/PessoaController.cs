using Efficacy.Api.Business;
using Efficacy.Api.DataAcess.Entities;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Efficacy.Api.Controllers
{
    /// <summary>
    /// Para trabalhar com os registros de Pessoas
    /// </summary>
    public class PessoaController: Controller
    {
        DbContextOptions<ProjetoAPIContext> contextOptions = null;
        public PessoaController(DbContextOptions<ProjetoAPIContext> options)
        {
            contextOptions = options;
        }  


        /// <summary>
        /// Buscar Pessoas: Para Pessoas no Sistema.
        /// </summary>
        /// <param name="queryOptions">Filtros de Pesquisa</param>
        /// <returns>Retorna o Resultado da Pesquisa</returns>
        [HttpGet]
        [Route("pessoa/buscar")]
        public ListarPessoasResponse ListarPessoas([FromQuery] ListarPessoasRequest listarPessoasRequest)
        {
            using(PessoaBusiness business = new PessoaBusiness(contextOptions))
            {
                return business.ListarPessoas(listarPessoasRequest);
            }
        }

        /// <summary>
        /// Gravar Pessoa: Para gravar uma Pessoa.
        /// </summary>
        /// <param name="gravarPessoaRequest">Dados necessários para a gravação do Registro.</param>
        /// <returns>Retorna o Resultado do Processamento e o ID do registro gravado. OBS.: O Processamento é executado com sucesso quando o Sucesso for igual a True.</returns>
        [HttpPost]
        [Route("pessoa/gravar")]
        public GravarPessoaResponse GravarPessoa([FromBody] GravarPessoaRequest gravarPessoaRequest)
        {
            using(PessoaBusiness business = new PessoaBusiness(contextOptions))
            {
                return business.GravarPessoa(gravarPessoaRequest);
            }
        }

        /// <summary>
        /// Excluir Pessoa: Para excluir uma Pessoa.
        /// </summary>
        /// <param name="ID">ID do registro a ser excluido.</param>
        /// <returns>Retorna o Resultado do Processamento. OBS.: O Processamento é executado com sucesso quando o Sucesso for igual a True.</returns>
        [HttpDelete]
        [Route("pessoa/excluir/{ID}")]
        public BaseResponse ExcluirPessoa(int ID)
        {
            using(PessoaBusiness business = new PessoaBusiness(contextOptions))
            {
                return business.ExcluirPessoa(ID);
            }
        }

        /// <summary>
        /// Excluir Pessoa Endereço: Para excluir o endereco de uma pessoa.
        /// </summary>
        /// <param name="ID">ID do registro a ser excluido.</param>
        /// <returns>Retorna o Resultado do Processamento. OBS.: O Processamento é executado com sucesso quando o Sucesso for igual a True.</returns>
        [HttpDelete]
        [Route("pessoa/endereco/excluir/{ID}")]
        public BaseResponse ExcluirEndereco(int ID)
        {
            using(PessoaBusiness business = new PessoaBusiness(contextOptions))
            {
                return business.ExcluirEndereco(ID);
            }
        }

    }
    
}
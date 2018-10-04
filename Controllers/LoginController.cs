using Efficacy.Api.Business;
using Efficacy.Api.DataAcess.Entities;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Efficacy.Api.Controllers
{
    /// <summary>
    /// Para validar usuário
    /// </summary>
    public class LoginController: Controller
    {
        DbContextOptions<ProjetoAPIContext> contextOptions = null;

        public LoginController(DbContextOptions<ProjetoAPIContext> options)
        {
            contextOptions = options;
        }

        /// <summary>
        /// Realizar Login.
        /// </summary>
        /// <param name="Email">Email do Usuário</param>
        /// <param name="Senha">Senha do Usuário</param>
        /// <returns>Retorna o Resultado do Processamento. OBS.: O Processamento é executado com sucesso quando o Sucesso for igual a True.</returns>
        [HttpPost]
        [Route("login/gravar")]
        public LoginResponse validarlogin ([FromBody] LoginRequest loginRequest)
        {
            using(LoginBusiness business = new LoginBusiness(contextOptions))
            {
                return business.ValidarLogin(loginRequest);
            }
        }  
    }
}
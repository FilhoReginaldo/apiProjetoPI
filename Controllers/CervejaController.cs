using Efficacy.Api.Business;
using Efficacy.Api.DataAcess.Entities;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Efficacy.Api.Controllers
{
     /// <summary>
    /// Para trabalhar com os registros de Cerveja
    /// </summary>
    public class CervejaController: Controller
    {
        DbContextOptions<ProjetoAPIContext> contextOptions = null;
        public CervejaController(DbContextOptions<ProjetoAPIContext> options)
        {
            contextOptions = options;
        }

    }
}
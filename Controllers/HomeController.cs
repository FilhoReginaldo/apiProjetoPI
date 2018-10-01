using Microsoft.AspNetCore.Mvc;

namespace Efficacy.Api.Controllers
{    
    
    public class HomeController : Controller
    {
        public ActionResult Index() => new RedirectResult("~/help");


    }

}
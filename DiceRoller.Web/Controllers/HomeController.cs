using Microsoft.AspNetCore.Mvc;

namespace DiceRoller.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
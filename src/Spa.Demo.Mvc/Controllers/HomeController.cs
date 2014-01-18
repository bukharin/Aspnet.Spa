using System.Web.Mvc;

namespace Spa.Demo.Mvc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Homr/

        public ActionResult Index()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace PostcodeSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Technical Test";

            return View();
        }
    }
}

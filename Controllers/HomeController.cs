using System.Web.Mvc;

namespace CarePlan.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Care Plans";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Title = "Test API";

            return View();
        }
    }
}

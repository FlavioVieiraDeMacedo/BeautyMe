using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyMe.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Pesquisar", "Servicos");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Pesquisar=" ")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Pesquisar", "Servicos");
            }
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
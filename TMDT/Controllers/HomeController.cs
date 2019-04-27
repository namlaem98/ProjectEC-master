using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMDT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDAO().ListByGroupID(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDAO().ListByGroupID(2);
            var loginmodel = new MenuDAO().ListByGroupID(3);
            ViewBag.LoginMenu = loginmodel;
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooter();
            return PartialView(model);
        }
    }
}
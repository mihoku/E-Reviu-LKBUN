using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using E_Reviu_LKBUN.Models;

namespace E_Reviu_LKBUN.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.risks = db.Risks.Count();
            ViewBag.scripts = db.UniverseRisks.Count();
            ViewBag.anomalies = db.AssuranceResult.Where(y => y.OutputColumnList.isAnomalyIdentifier && y.ColumnValue != 0).Count();
            ViewBag.universes = db.Universes.Count();
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

        public ActionResult Classification(string type, string loc, string controllerName, string actionName, string risktype)
        {
            if (type == "module")
            {
                ViewBag.type = loc;
                var classification2 = db.Modules.ToList();
                return PartialView("moduleClassification", classification2);
            }
            var classification = db.RiskCategories.ToList();
            ViewBag.actionName = actionName;
            ViewBag.controllerName = controllerName;
            ViewBag.risktype = risktype;
            ViewBag.type = loc;
            return PartialView("riskClassification", classification);
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}
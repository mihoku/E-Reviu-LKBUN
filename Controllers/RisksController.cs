using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Reviu_LKBUN.Models;

namespace E_Reviu_LKBUN.Controllers
{
    public class RisksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Risks
        public ActionResult Categories()
        {
            return View(db.RiskCategories.ToList());
        }

        // GET: Risks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory riskCategory = db.RiskCategories.Where(y=>y.abbreviation==id).First();
            if (riskCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.risktype = id;
            ViewBag.riskdescription = riskCategory.category;
            var risks = db.Risks.Where(y=>y.RiskCategoryID==riskCategory.ID).Include(r => r.RiskCategory);
            return View(risks.ToList());
        }

        public ActionResult Results(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory riskCategory = db.RiskCategories.Where(y => y.abbreviation == id).First();
            if (riskCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.risktype = id;
            ViewBag.Subtitle = riskCategory.category;
            //var risks = db.Risks.Where(y => y.RiskCategoryID == riskCategory.ID).Include(r => r.RiskCategory);
            return View(riskCategory);
        }

        // GET: Risks/Create
        public ActionResult CreateCategories()
        {
            return View();
        }

        // POST: Risks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategories([Bind(Include = "ID,category,abbreviation")] RiskCategory riskCategory)
        {
            if (ModelState.IsValid)
            {
                db.RiskCategories.Add(riskCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riskCategory);
        }

        // GET: Risks/Edit/5
        public ActionResult EditCategories(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory riskCategory = db.RiskCategories.Find(id);
            if (riskCategory == null)
            {
                return HttpNotFound();
            }
            return View(riskCategory);
        }

        // POST: Risks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategories([Bind(Include = "ID,category,abbreviation")] RiskCategory riskCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riskCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riskCategory);
        }

        // GET: Risks/Delete/5
        public ActionResult DeleteCategories(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskCategory riskCategory = db.RiskCategories.Find(id);
            if (riskCategory == null)
            {
                return HttpNotFound();
            }
            return View(riskCategory);
        }

        // POST: Risks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoriesConfirmed(int id)
        {
            RiskCategory riskCategory = db.RiskCategories.Find(id);
            db.RiskCategories.Remove(riskCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

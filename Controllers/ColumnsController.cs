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
    public class ColumnsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Columns
        //public ActionResult Index()
        //{
        //    var outputColumnLists = db.OutputColumnLists.Include(o => o.UniverseRisk);
        //    return View(outputColumnLists.ToList());
        //}

        // GET: Columns/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OutputColumnList outputColumnList = db.OutputColumnLists.Find(id);
        //    if (outputColumnList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(outputColumnList);
        //}

        // GET: Columns/Create
        public ActionResult AddIdentifier(int id)
        {
            ViewBag.UniverseRiskID = id;
            ViewBag.isValueColumn = false;
            ViewBag.isAnomalyIdentifier = false;
            ViewBag.UniverseID = db.UniverseRisks.Find(id).UniverseID;
            return View();
        }

        // POST: Columns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIdentifier([Bind(Include = "ID,UniverseRiskID,ColumnName,isValueColumn,isAnomalyIdentifier")] OutputColumnList outputColumnList)
        {
            ViewBag.UniverseID = db.UniverseRisks.Find(outputColumnList.UniverseRiskID).UniverseID;
            if (ModelState.IsValid)
            {
                db.OutputColumnLists.Add(outputColumnList);
                db.SaveChanges();
                return RedirectToAction("RiskDetails","Universes", new { id = outputColumnList.UniverseRiskID });
            }

            ViewBag.UniverseRiskID = outputColumnList.UniverseRiskID;
            ViewBag.isValueColumn = false;
            ViewBag.isAnomalyIdentifier = false;
            return View(outputColumnList);
        }

        public ActionResult AddValue(int id)
        {
            ViewBag.UniverseRiskID = id;
            ViewBag.isValueColumn = true;
            ViewBag.UniverseID = db.UniverseRisks.Find(id).UniverseID;
            return View();
        }

        // POST: Columns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddValue([Bind(Include = "ID,UniverseRiskID,ColumnName,isValueColumn,isAnomalyIdentifier")] OutputColumnList outputColumnList)
        {
            ViewBag.UniverseID = db.UniverseRisks.Find(outputColumnList.UniverseRiskID).UniverseID;
            if (ModelState.IsValid)
            {
                db.OutputColumnLists.Add(outputColumnList);
                db.SaveChanges();
                return RedirectToAction("RiskDetails", "Universes", new { id = outputColumnList.UniverseRiskID });
            }

            ViewBag.UniverseRiskID = outputColumnList.UniverseRiskID;
            ViewBag.isValueColumn = true;
            ViewBag.UniverseID = db.UniverseRisks.Find(outputColumnList.UniverseRiskID).UniverseID;
            return View(outputColumnList);
        }

        // GET: Columns/Edit/5
        public ActionResult EditIdentifier(int id)
        {
            OutputColumnList outputColumnList = db.OutputColumnLists.Find(id);
            if (outputColumnList == null)
            {
                return HttpNotFound();
            }
            ViewBag.UniverseRiskID = outputColumnList.UniverseRiskID;
            ViewBag.isValueColumn = outputColumnList.isValueColumn;
            ViewBag.isAnomalyIdentifier = outputColumnList.isAnomalyIdentifier;
            ViewBag.UniverseID = outputColumnList.UniverseRisk.UniverseID;
            return View(outputColumnList);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIdentifier([Bind(Include = "ID,UniverseRiskID,ColumnName,isValueColumn,isAnomalyIdentifier")] OutputColumnList outputColumnList)
        {
            ViewBag.UniverseID = db.UniverseRisks.Find(outputColumnList.UniverseRiskID).UniverseID;
            if (ModelState.IsValid)
            {
                db.Entry(outputColumnList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RiskDetails", "Universes", new { id = outputColumnList.UniverseRiskID });
            }
            ViewBag.UniverseRiskID = outputColumnList.UniverseRiskID;
            ViewBag.isValueColumn = outputColumnList.isValueColumn;
            ViewBag.isAnomalyIdentifier = outputColumnList.isAnomalyIdentifier;
            ViewBag.UniverseID = outputColumnList.UniverseRisk.UniverseID;
            return View(outputColumnList);
        }

        public ActionResult EditValue(int id)
        {
            OutputColumnList outputColumnList = db.OutputColumnLists.Find(id);
            if (outputColumnList == null)
            {
                return HttpNotFound();
            }
            ViewBag.UniverseRiskID = outputColumnList.UniverseRiskID;
            ViewBag.isValueColumn = outputColumnList.isValueColumn;
            ViewBag.UniverseID = outputColumnList.UniverseRisk.UniverseID;
            return View(outputColumnList);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditValue([Bind(Include = "ID,UniverseRiskID,ColumnName,isValueColumn,isAnomalyIdentifier")] OutputColumnList outputColumnList)
        {
            ViewBag.UniverseID = db.UniverseRisks.Find(outputColumnList.UniverseRiskID).UniverseID;
            if (ModelState.IsValid)
            {
                db.Entry(outputColumnList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RiskDetails", "Universes", new { id = outputColumnList.UniverseRiskID });
            }
            ViewBag.UniverseRiskID = outputColumnList.UniverseRiskID;
            ViewBag.isValueColumn = outputColumnList.isValueColumn;
            ViewBag.UniverseID = outputColumnList.UniverseRisk.UniverseID;
            return View(outputColumnList);
        }

        // GET: Columns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutputColumnList outputColumnList = db.OutputColumnLists.Find(id);
            if (outputColumnList == null)
            {
                return HttpNotFound();
            }
            return View(outputColumnList);
        }

        // POST: Columns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OutputColumnList outputColumnList = db.OutputColumnLists.Find(id);
            db.OutputColumnLists.Remove(outputColumnList);
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

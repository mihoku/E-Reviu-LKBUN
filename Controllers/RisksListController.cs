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
    public class RisksListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RisksList
        public ActionResult Index(int? id, int?mod, string type)
        {
            if (type == "all")
            {
                var risks4 = db.Risks.Include(r => r.RiskCategory);
                ViewBag.Subtitle = "Semua Risiko";
                return View(risks4.ToList());
            }
            else if (id != null)
            {
                var risks2 = db.Risks.Where(y=>y.RiskCategoryID==id).Include(r => r.RiskCategory);
                ViewBag.Subtitle = db.RiskCategories.Find(id).category;
                return View(risks2.ToList());
            }
            else if (mod != null)
            {
                var risks3 = db.Risks.Where(y => y.ModuleID == mod).Include(r => r.RiskCategory);
                ViewBag.Subtitle = db.Modules.Find(mod).ModuleName;
                return View(risks3.ToList());
            }
            var risks = db.Risks.Include(r => r.RiskCategory);
            ViewBag.Subtitle = "Semua Risiko";
            return View(risks.ToList());
        }

        // GET: RisksList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risk risk = db.Risks.Find(id);
            if (risk == null)
            {
                return HttpNotFound();
            }
            return View(risk);
        }

        // GET: RisksList/Create
        public ActionResult Create()
        {
            ViewBag.RiskCategoryID = new SelectList(db.RiskCategories, "ID", "category");
            ViewBag.ModuleID = new SelectList(db.Modules, "ID", "ModuleName");
            return View();
        }

        // POST: RisksList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Reference,Code,Tujuan,Risiko,Pengendalian,Teknik,Atribut,RiskCategoryID,ModuleID")] Risk risk)
        {

            var codeExistCount = db.Risks.Where(y => y.Code == risk.Code).Count();
            var referenceExistCount = db.Risks.Where(y => y.Reference == risk.Reference).Count();
            ViewBag.RiskCategoryID = new SelectList(db.RiskCategories, "ID", "category", risk.RiskCategoryID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ID", "ModuleName", risk.ModuleID);

            if (!ModelState.IsValid)
            {
                return View(risk);

            }

            else if (codeExistCount > 0)
            {
                ViewBag.CodeError = "Kode Risiko sudah digunakan.";
                if (referenceExistCount > 0)
                {
                    ViewBag.ReferenceError = "Kode Referensi sudah digunakan.";
                    return View(risk);
                }
                return View(risk);
            }

            else if (referenceExistCount > 0)
            {
                ViewBag.ReferenceError = "Kode Referensi sudah digunakan.";
                if (codeExistCount > 0)
                {
                    ViewBag.CodeError = "Kode Risiko sudah digunakan.";
                    return View(risk);
                }
                return View(risk);
            }

            db.Risks.Add(risk);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: RisksList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risk risk = db.Risks.Find(id);
            if (risk == null)
            {
                return HttpNotFound();
            }
            ViewBag.RiskCategoryID = new SelectList(db.RiskCategories, "ID", "category", risk.RiskCategoryID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ID", "ModuleName", risk.ModuleID);
            ViewBag.riskCode = risk.Code;
            ViewBag.referenceCode = risk.Reference;
            return View(risk);
        }

        // POST: RisksList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Reference,Code,Tujuan,Risiko,Pengendalian,Teknik,Atribut,RiskCategoryID,ModuleID")] Risk riskbind)
        {
            
            var codeExistCount = db.Risks.Where(y => y.Code == riskbind.Code && y.ID!=riskbind.ID).Count();
            var referenceExistCount = db.Risks.Where(y => y.Reference == riskbind.Reference && y.ID != riskbind.ID).Count();
            if (ModelState.IsValid && codeExistCount==0 && referenceExistCount==0)
            {
                db.Entry(riskbind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = riskbind.RiskCategoryID });
            }

            else
            {
                Risk risk = db.Risks.Find(riskbind.ID);
                ViewBag.riskCode = risk.Code;
                ViewBag.referenceCode = risk.Reference;
                ViewBag.RiskCategoryID = new SelectList(db.RiskCategories, "ID", "category", riskbind.RiskCategoryID);
                ViewBag.ModuleID = new SelectList(db.Modules, "ID", "ModuleName", riskbind.ModuleID);

                if (codeExistCount > 0)
                {
                    ViewBag.CodeError = "Kode Risiko sudah digunakan.";
                    if (referenceExistCount > 0)
                    {
                        ViewBag.ReferenceError = "Kode Referensi sudah digunakan.";
                        return View(riskbind);
                    }
                    return View(riskbind);
                }

                else if (referenceExistCount > 0)
                {
                    ViewBag.ReferenceError = "Kode Referensi sudah digunakan.";
                    if (codeExistCount > 0)
                    {
                        ViewBag.CodeError = "Kode Risiko sudah digunakan.";
                        return View(riskbind);
                    }
                    return View(riskbind);
                }

                return View(riskbind);
            }
            
        }

        // GET: RisksList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risk risk = db.Risks.Find(id);
            if (risk == null)
            {
                return HttpNotFound();
            }
            return View(risk);
        }

        // POST: RisksList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Risk risk = db.Risks.Find(id);
            db.Risks.Remove(risk);
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

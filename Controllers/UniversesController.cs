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
    public class UniversesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Universes
        public ActionResult Index(int id, int?unit)
        {
            if (unit != null)
            {
                var unitData = db.Units.Find(unit);
                var unitverses = db.Universes.Include(u => u.Unit).Where(y => y.unitID == unit);
                ViewBag.Subtitle = unitData.Name;
                ViewBag.key = "unit";
                return View(unitverses.ToList());
            }
            ViewBag.key = "notunit";
            var universes = db.Universes.Include(u => u.Unit).Where(y => y.parentID == id);
            ViewBag.parentID = id;
            if (id == 0)
            {
                return View(universes.ToList());
            }
            var parentUniverse = db.Universes.Find(id);
            ViewBag.grandparentID = parentUniverse.parentID;
            ViewBag.Subtitle = parentUniverse.ShortName;
            return View(universes.ToList());
        }

        public ActionResult List()
        {
            var units = db.Units;
            return PartialView(units.ToList());
        }

        // GET: Universes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universe universe = db.Universes.Find(id);
            if (universe == null)
            {
                return HttpNotFound();
            }
            return View(universe);
        }

        public ActionResult RiskDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniverseRisk risk = db.UniverseRisks.Find(id);
            Universe universe = db.Universes.Find(risk.UniverseID);
            ViewBag.universeRiskID = risk.ID;
            if (universe == null)
            {
                return HttpNotFound();
            }
            return View(universe);
        }

        // GET: Universes/Create
        public ActionResult Create(int id)
        {
            ViewBag.parentID = id;
            var parentUniverse = db.Universes.Find(id);
            ViewBag.Subtitle = parentUniverse.ShortName;
            ViewBag.parentCode = parentUniverse.Code;
            ViewBag.unitID = new SelectList(db.Units, "ID", "Name");
            
            return View();
        }

        // POST: Universes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ShortName,LongName,Code,parentCode,parentID,unitID,ReportTemplateAvailable")] Universe universe)
        {
            if (ModelState.IsValid)
            {
                db.Universes.Add(universe);
                db.SaveChanges();
                return RedirectToAction("Index", new { id=universe.parentID });
            }

            ViewBag.parentID = universe.parentID;
            var parentUniverse = db.Universes.Find(universe.parentID);
            ViewBag.parentCode = parentUniverse.Code;
            ViewBag.Subtitle = parentUniverse.ShortName;
            ViewBag.unitID = new SelectList(db.Units, "ID", "Name", universe.unitID);
            return View(universe);
        }

        // GET: Universes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universe universe = db.Universes.Find(id);
            if (universe == null)
            {
                return HttpNotFound();
            }
            ViewBag.unitID = new SelectList(db.Units, "ID", "Name", universe.unitID);
            return View(universe);
        }

        // POST: Universes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ShortName,LongName,Code,parentCode,parentID,unitID,ReportTemplateAvailable")] Universe universe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=universe.parentID });
            }
            ViewBag.unitID = new SelectList(db.Units, "ID", "Name", universe.unitID);
            return View(universe);
        }

        // GET: Universes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universe universe = db.Universes.Find(id);
            if (universe == null)
            {
                return HttpNotFound();
            }
            return View(universe);
        }

        // POST: Universes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Universe universe = db.Universes.Find(id);
            var parID = universe.parentID;
            db.Universes.Remove(universe);
            db.SaveChanges();
            return RedirectToAction("Index", new { id=parID });
        }

        public ActionResult AddRisk(int id)
        {
            ViewBag.RiskCategoryID = new SelectList(db.RiskCategories, "ID", "category");
            ViewBag.ModuleID = new SelectList(db.Modules, "ID", "ModuleName");
            ViewBag.Subtitle = db.Universes.Find(id).ShortName;
            ViewBag.UniverseID = id;
            return View();
        }

        // POST: RisksList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRisk([Bind(Include = "ID,Reference,Code,Tujuan,Risiko,Pengendalian,Teknik,Atribut,RiskCategoryID,ModuleID,UniverseID,Script,LangkahPengujian")] RiskUniverseModel risk)
        {

            var codeExistCount = db.Risks.Where(y => y.Code == risk.Code).Count();
            var referenceExistCount = db.Risks.Where(y => y.Reference == risk.Reference).Count();
            ViewBag.RiskCategoryID = new SelectList(db.RiskCategories, "ID", "category", risk.RiskCategoryID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ID", "ModuleName", risk.ModuleID);
            ViewBag.Subtitle = db.Universes.Find(risk.UniverseID).ShortName;
            ViewBag.UniverseID = risk.UniverseID;

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

            var riskdata = new Risk()
            {
                Reference = risk.Reference,
                Code = risk.Code,
                Risiko = risk.Risiko,
                Pengendalian = risk.Pengendalian,
                Teknik = risk.Teknik,
                Tujuan = risk.Tujuan,
                Atribut = risk.Atribut,
                RiskCategoryID = risk.RiskCategoryID,
                ModuleID = risk.ModuleID
            };

            db.Risks.Add(riskdata);
            db.SaveChanges();

            var unirisk = new UniverseRisk()
            {
                UniverseID = risk.UniverseID,
                RiskID = riskdata.ID,
                Script = risk.Script,
                LangkahPengujian = risk.LangkahPengujian
            };

            db.UniverseRisks.Add(unirisk);
            db.SaveChanges();

            return RedirectToAction("Details",new { id=risk.UniverseID });

        }

        // GET: Universes/Edit/5
        public ActionResult ModifyScript(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniverseRisk universe = db.UniverseRisks.Find(id);
            if (universe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subtitle = universe.Risk.Code + " " + universe.Universe.ShortName;
            return View(universe);
        }

        // POST: Universes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyScript([Bind(Include = "ID,Script,RiskID,UniverseID")] UniverseRisk universe)
        {
            if (ModelState.IsValid)
            {
                UniverseRisk universeEdited = db.UniverseRisks.Find(universe.ID);
                universeEdited.Script = universe.Script;
                //db.Entry(universe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = universe.UniverseID });
            }
            ViewBag.Subtitle = universe.Risk.Code + " " + universe.Universe.ShortName;
            return View(universe);
        }

        public ActionResult ModifyDescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniverseRisk universe = db.UniverseRisks.Find(id);
            if (universe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subtitle = universe.Risk.Code + " " + universe.Universe.ShortName;
            return View(universe);
        }

        // POST: Universes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyDescription([Bind(Include = "ID,LangkahPengujian,RiskID,UniverseID")] UniverseRisk universe)
        {
            if (ModelState.IsValid)
            {
                UniverseRisk universeEdited = db.UniverseRisks.Find(universe.ID);
                universeEdited.LangkahPengujian = universe.LangkahPengujian;
                //db.Entry(universe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = universe.UniverseID });
            }
            ViewBag.Subtitle = universe.Risk.Code + " " + universe.Universe.ShortName;
            return View(universe);
        }

        public ActionResult SelectRisk(int id)
        {
            ViewBag.UniverseID = id;
            ViewBag.Subtitle = db.Universes.Find(id).ShortName;
            var risks = db.Risks.Where(y => y.UniverseRisk.Where(x => x.UniverseID == id).Count() == 0).ToList();
            return View(risks);
        }

        public ActionResult PickRisk(int id, int uni)
        {
            var unirisk = new UniverseRisk()
            {
                UniverseID = uni,
                RiskID = id,
                Script = "",
                LangkahPengujian = ""
            };

            db.UniverseRisks.Add(unirisk);
            db.SaveChanges();
            return RedirectToAction("ConstructScript", new { id=unirisk.ID });
        }

        public ActionResult ConstructScript(int id)
        {
            var universe = db.UniverseRisks.Find(id);
            ViewBag.RiskID = universe.RiskID;
            ViewBag.UniverseID = universe.UniverseID;
            ViewBag.Subtitle = universe.Risk.Code + " " + universe.Universe.ShortName;
            return View(universe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConstructScript([Bind(Include = "ID,Script,LangkahPengujian,RiskID,UniverseID")] UniverseRisk universe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = universe.UniverseID });
            }
            ViewBag.Subtitle = universe.Risk.Code + " " + universe.Universe.ShortName;
            return View(universe);
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

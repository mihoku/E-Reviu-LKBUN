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
    public class AssurancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assurances
        public ActionResult Index()
        {
            var assurance = db.Assurance.Include(a => a.Universe);
            return View(assurance.ToList());
        }

        public ActionResult Reports()
        {
            var assurance = db.Assurance.Include(a => a.Universe);
            return View("Index",assurance.ToList());
        }

        public ActionResult PickUnit(string id, string org)
        {
            if (org == "KPPN")
            {
                var KPPN = db.KPPN.Where(y => y.KodeKPPN == id).First();
                return Content(KPPN.NamaKPPN);
            }
            else
            {
                var Satker = db.Satker.Where(y => y.KodeSatker == id).First();
                return Content(Satker.NamaSatker);
            }
        }

        public ActionResult Add()
        {
            var universes = db.Universes.Include(u => u.Unit);
            return View(universes.ToList());
        }

        // GET: Assurances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurance.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            return View(assurance);
        }

        public ActionResult Preview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurance.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }

            return View(assurance);
        }

        public ActionResult CHR(int id)
        {
            
            Assurance assurance = db.Assurance.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (assurance.Universe.ReportTemplateAvailable)
                {
                    var viewpath = "~/Views/Reports/"+assurance.Universe.Code+".cshtml";
                    return PartialView(viewpath,assurance);
                }
                else
                {
                    return PartialView("~/Views/Reports/None.cshtml",assurance);
                }
            }

            
        }

        public ActionResult ListKPPN(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var KPPN = db.KPPN.ToList();

            Assurance assurance = db.Assurance.Find(id);

            ViewBag.AssuranceID = id;
            ViewBag.ST = assurance.ST;
            ViewBag.ShortName = assurance.Universe.ShortName;
            ViewBag.SPANReportPeriod = assurance.SPANReportPeriod;
            return View(KPPN);
        }

        public ActionResult PreviewKPPN(int? id, string code)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurance.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            ViewBag.KPPN = code;
            return View(assurance);
        }

        public ActionResult Response(int id)
        {
            Assurance assurance = db.Assurance.Find(id);
            var area = db.UniverseDetails.Where(y => y.UniverseID == assurance.UniverseID).ToList();
            ViewBag.AssuranceID = id;
            ViewBag.Subtitle = assurance.ST;
            return View(area);
        }

        public ActionResult ResponseAdd(int id, int area)
        {
            
            ViewBag.AssuranceID = id;
            ViewBag.UniverseDetailID = area;
            Assurance assurance = db.Assurance.Find(id);
            ViewBag.Subtitle = assurance.ST;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResponseAdd([Bind(Include = "ID,AssuranceID,UniverseDetailID,Content")] Responses response)
        {
           
            if (ModelState.IsValid)
            {
                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("Response", new { id = response.AssuranceID });
            }
            var assurance = db.Assurance.Find(response.AssuranceID);
            ViewBag.Subtitle = assurance.ST;
            ViewBag.AssuranceID = response.AssuranceID;
            ViewBag.UniverseDetailID = response.UniverseDetailID;
            return View(response);
        }

        // GET: Responses/Edit/5
        public ActionResult ResponseEdit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responses responses = db.Responses.Find(id);
            if (responses == null)
            {
                return HttpNotFound();
            }
            var assurance = db.Assurance.Find(responses.AssuranceID);
            ViewBag.Subtitle = assurance.ST;
            ViewBag.AssuranceID = responses.AssuranceID;
            ViewBag.UniverseDetailID = responses.UniverseDetailID;
            return View(responses);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResponseEdit([Bind(Include = "ID,AssuranceID,UniverseDetailID,Content")] Responses responses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Response", new { id = responses.AssuranceID });
            }
            ViewBag.AssuranceID = responses.AssuranceID;
            ViewBag.UniverseDetailID = responses.UniverseDetailID;
            Assurance assurance = db.Assurance.Find(responses.AssuranceID);
            ViewBag.Subtitle = assurance.ST;
            return View(responses);
        }

        public ActionResult DetailsDash()
        {
            Assurance assurance = db.Assurance.OrderByDescending(y=>y.ID).First();
            if (assurance == null)
            {
                return HttpNotFound();
            }
            return PartialView(assurance);
        }

        // GET: Assurances/Create
        public ActionResult Create(int id)
        {
            List<SelectListModel> Periodselect = new List<SelectListModel>();
            Periodselect.Add(new SelectListModel() { Text = "JAN", Value = 1 });
            Periodselect.Add(new SelectListModel() { Text = "FEB", Value = 2 });
            Periodselect.Add(new SelectListModel() { Text = "MAR", Value = 3 });
            Periodselect.Add(new SelectListModel() { Text = "APR", Value = 4 });
            Periodselect.Add(new SelectListModel() { Text = "JUN", Value = 6 });
            Periodselect.Add(new SelectListModel() { Text = "JUL", Value = 7 });
            Periodselect.Add(new SelectListModel() { Text = "AUG", Value = 8 });
            Periodselect.Add(new SelectListModel() { Text = "SEP", Value = 9 });
            Periodselect.Add(new SelectListModel() { Text = "OCT", Value = 10 });
            Periodselect.Add(new SelectListModel() { Text = "NOV", Value = 11 });
            Periodselect.Add(new SelectListModel() { Text = "DEC", Value = 12 });
            ViewBag.UniverseID = id;
            ViewBag.Date = DateTime.Now;
            ViewBag.Periode = new SelectList(Periodselect, "Value", "Text");
            return View();
        }

        // POST: Assurances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ST,UniverseID,Periode,Tahun")] AddProjectModel assurance)
        {
            List<SelectListModel> Periodselect = new List<SelectListModel>();
            Periodselect.Add(new SelectListModel() { Text = "JAN", Value = 1 });
            Periodselect.Add(new SelectListModel() { Text = "FEB", Value = 2 });
            Periodselect.Add(new SelectListModel() { Text = "MAR", Value = 3 });
            Periodselect.Add(new SelectListModel() { Text = "APR", Value = 4 });
            Periodselect.Add(new SelectListModel() { Text = "JUN", Value = 6 });
            Periodselect.Add(new SelectListModel() { Text = "JUL", Value = 7 });
            Periodselect.Add(new SelectListModel() { Text = "AUG", Value = 8 });
            Periodselect.Add(new SelectListModel() { Text = "SEP", Value = 9 });
            Periodselect.Add(new SelectListModel() { Text = "OCT", Value = 10 });
            Periodselect.Add(new SelectListModel() { Text = "NOV", Value = 11 });
            Periodselect.Add(new SelectListModel() { Text = "DEC", Value = 12 });
            
            if (ModelState.IsValid)
            {
                var yearcut = assurance.Tahun - 2000;
                var yearprev = yearcut - 1;
                
                Assurance data = new Assurance()
                {
                    ID=1,
                    ST = assurance.ST,
                    UniverseID = assurance.UniverseID,
                    //Date = assurance.Date,
                    ErekonReportYear = assurance.Tahun,
                    SPANBeginReportPeriod = "JAN-" + yearcut.ToString(),
                    SPANPreviousReportPeriod = "AD2-"+yearprev.ToString(),
                    SPANReportPeriod = Periodselect.Where(y=>y.Value==assurance.Periode).First().Text+ "-" + yearcut.ToString(),
                    ErekonReportMonth = assurance.Periode
                };
                //return RedirectToAction("Index");
                db.Assurance.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UniverseID = assurance.UniverseID;
            ViewBag.Periode = new SelectList(Periodselect, "Value", "Text");
            ViewBag.Date = DateTime.Now;
            return View(assurance);
        }

        public ActionResult PopulateScript(int id)
        {
            var project = db.Assurance.Find(id);
            ViewBag.ProjectID = project.ID;
            ViewBag.SPANReportPeriod = project.SPANReportPeriod;
            ViewBag.SPANBeginReportPeriod = project.SPANBeginReportPeriod;
            ViewBag.SPANPreviousReportPeriod = project.SPANPreviousReportPeriod;
            ViewBag.ErekonReportMonth = project.ErekonReportMonth;
            ViewBag.ErekonReportYear = project.ErekonReportYear;
            var universeRisk = db.UniverseRisks.Where(y => y.UniverseID == project.UniverseID).ToList();
            ViewBag.lk = project.Universe.ShortName;
            return View(universeRisk);
        }

        public ActionResult ProcessingScript(int id)
        {
            var project = db.Assurance.Find(id);
            ViewBag.ProjectID = project.ID;
            ViewBag.SPANReportPeriod = project.SPANReportPeriod;
            ViewBag.ErekonReportMonth = project.ErekonReportMonth;
            ViewBag.ErekonReportYear = project.ErekonReportYear;
            var universeRisk = db.UniverseRisks.Where(y => y.UniverseID == project.UniverseID&&y.OutputColumnList.Count()!=0).ToList();
            ViewBag.lk = project.Universe.ShortName;
            return View(universeRisk);
        }

        public ActionResult RiskProcessingScript(int id,int c)
        {
            var project = db.Assurance.Find(id);
            ViewBag.ProjectID = project.ID;
            ViewBag.SPANReportPeriod = project.SPANReportPeriod;
            ViewBag.ErekonReportMonth = project.ErekonReportMonth;
            ViewBag.ErekonReportYear = project.ErekonReportYear;
            var universeRisk = db.UniverseRisks.Where(y => y.ID==c&&y.OutputColumnList.Count() != 0).ToList();
            ViewBag.lk = project.Universe.ShortName;
            return View("ProcessingScript",universeRisk);
        }

        public ActionResult ColumnIdentifier(int id)
        {
            var columnName = db.OutputColumnLists.Where(y => !y.isValueColumn && y.UniverseRiskID == id).OrderBy(y => y.ColumnName).ToList();
            return PartialView("ColumnIdentifier", columnName);
        }

        public ActionResult AnomalyCounterPerTest(int id, int r)
        {
            var riskUniverse = db.UniverseRisks.Find(r);
            var result = db.AssuranceResult.Where(y => y.AssuranceID == id && y.RiskCode == riskUniverse.Risk.Code && y.OutputColumnList.isAnomalyIdentifier&&y.ColumnValue!=0).Count();
            return Content(result.ToString());
        }

        public ActionResult AnomalyCounterPerProject(int id)
        {
            var result = db.AssuranceResult.Where(y => y.AssuranceID == id && y.OutputColumnList.isAnomalyIdentifier && y.ColumnValue != 0).Count();
            return Content(result.ToString());
        }

        public ActionResult Results(int id, int c)
        {
            var project = db.Assurance.Find(id);
            ViewBag.ST = project.ST;
            ViewBag.ProjectID = project.ID;
            var universeRisk = db.UniverseRisks.Find(c);
            return View(universeRisk);
        }

        public ActionResult ResultsPopulation(int id, int c)
        {
            
            var universeRisk = db.UniverseRisks.Find(c);
            var columns = db.OutputColumnLists.Where(y => y.UniverseRiskID == c);
            ViewBag.indentifierCount = columns.Where(y => !y.isValueColumn).Count();
            
            var results = db.AssuranceResult.Where(y => y.AssuranceID == id && y.RiskCode == universeRisk.Risk.Code);
            return PartialView(results);
        }

        // GET: Assurances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurance.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            ViewBag.UniverseID = new SelectList(db.Universes, "ID", "ShortName", assurance.UniverseID);
            return View(assurance);
        }

        // POST: Assurances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ST,UniverseID,Date,SPANReportPeriod,SPANBeginReportPeriod,SPANPreviousReportPeriod,ErekonReportMonth,ErekonReportYear")] Assurance assurance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assurance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniverseID = new SelectList(db.Universes, "ID", "ShortName", assurance.UniverseID);
            return View(assurance);
        }

        // GET: Assurances/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Assurance assurance = db.Assurance.Find(id);
        //    if (assurance == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(assurance);
        //}

        //// POST: Assurances/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Assurance assurance = db.Assurance.Find(id);
        //    db.Assurance.Remove(assurance);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

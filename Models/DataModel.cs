using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace E_Reviu_LKBUN.Models
{
    public class ActiveSession
    {
        public int ID { get; set; }        
        [EmailAddress]
        public string UserName { get; set; }
        public int STID { get; set; }
        public int periodID { get; set; }
        [ForeignKey("periodID")]
        public virtual Period Period { get; set; }
    }

    public class ST
    {
        public int ID { get; set; }
        public string Nomor { get; set; }
        public DateTime TanggalAwal { get; set; }
        public DateTime TanggalAkhir { get; set; }
        public int UniverseID { get; set; }
        [ForeignKey("UniverseID")]
        public virtual Universe Universe { get; set; }
    }

    public class Universe
    {
        public Universe()
        {
            this.UniverseRisk = new HashSet<UniverseRisk>();
            this.ST = new HashSet<ST>();
            this.Assurance = new HashSet<Assurance>();
            this.UniverseDetails = new HashSet<UniverseDetails>();
        }
        public int ID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Code { get; set; }
        public string parentCode { get; set; }
        public int parentID { get; set; }
        public int unitID { get; set; }
        public bool ReportTemplateAvailable { get; set; }
        public virtual ICollection<UniverseRisk> UniverseRisk { get; set; }
        public virtual ICollection<ST> ST { get; set; }
        [ForeignKey("unitID")]
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Assurance> Assurance { get; set; }
        public virtual ICollection<UniverseDetails> UniverseDetails { get; set; }
    }

    public class Unit
    {
        public Unit()
        {
            this.Universe = new HashSet<Universe>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public virtual ICollection<Universe> Universe { get; set; }
    }

    public class Risk
    {
        public Risk()
        {
            this.UniverseRisk = new HashSet<UniverseRisk>();
        }
        public int ID { get; set; }
        public string Reference { get; set; }
        public string Code { get; set; }
        public string Tujuan { get; set; }
        public string Risiko { get; set; }
        public string Pengendalian { get; set; }
        public string Teknik { get; set; }
        public string Atribut { get; set; }
        public int RiskCategoryID { get; set; }
        public int ModuleID { get; set; }
        [ForeignKey("RiskCategoryID")]
        public virtual RiskCategory RiskCategory { get; set; }
        [ForeignKey("ModuleID")]
        public virtual Module Module { get; set; }
        public virtual ICollection<UniverseRisk> UniverseRisk { get; set; }
    }

    public class UniverseRisk
    {
        public UniverseRisk()
        {
            this.OutputColumnList = new HashSet<OutputColumnList>();
        }
        public int ID { get; set; }
        public int UniverseID { get; set; }
        public int RiskID { get; set; }
        public string Script { get; set; }
        [AllowHtml]
        public string LangkahPengujian { get; set; }
        [ForeignKey("RiskID")]
        public virtual Risk Risk { get; set; }
        [ForeignKey("UniverseID")]
        public virtual Universe Universe { get; set; }
        public virtual ICollection<OutputColumnList> OutputColumnList { get; set; }
    }

    public class OutputColumnList
    {
        public OutputColumnList()
        {
            this.AssuranceResults = new HashSet<AssuranceResult>();
        }
        public int ID { get; set; }
        public int UniverseRiskID { get; set; }
        public string ColumnName { get; set; }
        public bool isValueColumn { get; set; }
        public bool isAnomalyIdentifier { get; set; }
        [ForeignKey("UniverseRiskID")]
        public virtual UniverseRisk UniverseRisk { get; set; }
        public virtual ICollection<AssuranceResult> AssuranceResults { get; set; }
    }

    public class RiskCategory
    {
        public RiskCategory()
        {
            this.Risk = new HashSet<Risk>();
        }
        public int ID { get; set; }
        public string category { get; set; }
        public string abbreviation { get; set; }
        public virtual ICollection<Risk> Risk { get; set; }
    }

    public class Period
    {
        public Period()
        {
            this.ActiveSessions = new HashSet<ActiveSession>();
        }
        public int ID { get; set; }
        public int description { get; set; }
        public virtual ICollection<ActiveSession> ActiveSessions { get; set; }
    }

    public class Module
    {
        public Module()
        {
            this.Modules = new HashSet<Module>();
        }
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public string abbreviation { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }

    public class Assurance
    {
        public Assurance()
        {
            this.AssuranceResult = new HashSet<AssuranceResult>();
            this.Responses = new HashSet<Responses>();
        }
        public int ID { get; set; }
        public string ST { get; set; }
        public int UniverseID { get; set; }
        //public DateTime ? Date { get; set; }
        public string SPANReportPeriod { get; set; }
        public string SPANBeginReportPeriod { get; set; }
        public string SPANPreviousReportPeriod { get; set; }
        public int ErekonReportMonth { get; set; }
        public int ErekonReportYear { get; set; }
        [ForeignKey("UniverseID")]
        public virtual Universe Universe { get; set; }
        public virtual ICollection<AssuranceResult> AssuranceResult { get; set; }
        public virtual ICollection<Responses> Responses { get; set; }
    }

    public class AssuranceResult
    {
        public int ID { get; set; }
        public int AssuranceID { get; set; }
        public string RiskCode { get; set; }
        public string Identifier1 { get; set; }
        public string Identifier2 { get; set; }
        public string Identifier3 { get; set; }
        public string Identifier4 { get; set; }
        public string Identifier5 { get; set; }
        public string Identifier6 { get; set; }
        public string Identifier7 { get; set; }
        public int ValueID { get; set; }
        public string ColumnName { get; set; }
        public decimal ColumnValue { get; set; }
        public DateTime ExecutionDate { get; set; }
        [ForeignKey("AssuranceID")]
        public virtual Assurance Assurance { get; set; }
        [ForeignKey("ValueID")]
        public virtual OutputColumnList OutputColumnList { get; set; }
    }

    public class KPPN
    {
        public int ID { get; set; }
        public string KodeKPPN { get; set; }
        public string NamaKPPN { get; set; }
    }

    public class Satker
    {
        public int ID { get; set; }
        public string KodeSatker { get; set; }
        public string NamaSatker { get; set; }
    }

    public class UniverseDetails
    {
        public UniverseDetails()
        {
            this.Responses = new HashSet<Responses>();
        }
        public int ID { get; set; }
        public int UniverseID { get; set; }
        public string Area { get; set; }
        public string AreaName { get; set; }
        [ForeignKey("UniverseID")]
        public virtual Universe Universe { get; set; }
        public virtual ICollection<Responses> Responses { get; set; }
    }

    public class Responses
    {
        public int ID { get; set; }
        public int AssuranceID { get; set; }
        public int UniverseDetailID { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        [ForeignKey("UniverseDetailID")]
        public virtual UniverseDetails UniverseDetails { get; set; }
        [ForeignKey("AssuranceID")]
        public virtual Assurance Assurance { get; set; }
    }


}
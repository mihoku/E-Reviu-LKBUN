using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace E_Reviu_LKBUN.Models
{
    public class AddProjectModel
    {
        public string ST { get; set; }
        public int UniverseID { get; set; }
        //public DateTime Date { get; set; }
        public int Periode { get; set; }
        public int Tahun { get; set; }
    }

    public class Result1
    {
        public string Identifier1 { get; set; }
    }

    public class Result2
    {
        public string Identifier1 { get; set; }
        public string Identifier2 { get; set; }
    }

    public class Result3
    {
        public string Identifier1 { get; set; }
        public string Identifier2 { get; set; }
        public string Identifier3 { get; set; }
    }

    public class Result4
    {
        public string Identifier1 { get; set; }
        public string Identifier2 { get; set; }
        public string Identifier3 { get; set; }
        public string Identifier4 { get; set; }
    }

    public class SelectListModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class RiskUniverseModel
    {
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
        public int UniverseID { get; set; }
        public string Script { get; set; }
        [System.Web.Mvc.AllowHtml]
        public string LangkahPengujian { get; set; }
    }
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}
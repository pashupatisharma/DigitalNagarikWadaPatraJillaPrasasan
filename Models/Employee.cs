using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Employee
    {
        [Key()]
        public int Id { get; set; }
        [Display(Name = "नाम")]
        public string AdkshyaName { get; set; }
        [Display(Name = "सम्पर्क नं")]
        public string AdkshyaContactNo { get; set; }
        [Display(Name = "संदेश")]
        public  string AdkshyaMessage { get; set; }
        [Display(Name = "फोटो")]

        public byte[] AdkshyaPhoto { get; set; }
        [NotMapped]
        public HttpPostedFileBase Photo1 { get; set; }
        [Display(Name = "नाम")]
        public string UpaAdkshyaName { get; set; }
        [Display(Name = "सम्पर्क नं")]
        public string UpaAdkshyaContactNo { get; set; }
        [Display(Name = "संदेश")]
        public string UpaAdkshyaMessage { get; set; }
        [Display(Name = "फोटो")]
        public byte[] UpaAdkshyaPhoto { get; set; }
        [Display(Name = "फोटो")]
        [NotMapped]
        public HttpPostedFileBase Photo2 { get; set; }
        [Display(Name = "नाम")]
        public string AdhikritName { get; set; }
        [Display(Name = "सम्पर्क नं")]
        public string AdhikritContactNo { get; set; }
        [Display(Name = "संदेश")]
        public string AdhikritMessage { get; set; }
        [Display(Name = "फोटो")]
        public byte[] AdhikritPhoto { get; set; }
        [NotMapped]
        [Display(Name = "फोटो")]
        public HttpPostedFileBase Photo3 { get; set; }



        [Display(Name = "नाम")]
        public string ITName { get; set; }
        [Display(Name = "सम्पर्क नं")]
        public string ITContactNo { get; set; }
        [Display(Name = "संदेश")]
        public string ITMessage { get; set; }
        [Display(Name = "फोटो")]
        public byte[] ItPhoto { get; set; }
        [NotMapped]
        [Display(Name = "फोटो")]
        public HttpPostedFileBase Photo4 { get; set; }
    }
}
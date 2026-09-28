using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{

    public partial class Offices
    {
        [Key()]
        public int Office_ID { get; set; }
        [Display(Name = "कोड")]
        public string Office_Code { get; set; }
        [Display(Name = "नाम")]
        public string Office_Name { get; set; }
        [Display(Name = "ठेगाना")]
        public string Office_Location { get; set; }
        [Display(Name = "सम्पर्क नं")]
        public string Office_Phone { get; set; }
        [Display(Name = "वेबसाइट")]
        public string Office_URL { get; set; }
        [Display(Name = "इमेल")]
        public string Office_Email { get; set; }
        [Display(Name = "Marquee Message")]
        public string Message { get; set; }
   
       


    }
}

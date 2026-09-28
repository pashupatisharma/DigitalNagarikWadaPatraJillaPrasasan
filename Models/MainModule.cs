using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{

    public class HomePageViewModel
    {
        public MainModule mainModule { get; set; }
        public SubModule subModule { get; set; }
        public Video video { get; set; }
        public Notice notice { get; set; }
        public EmployeeSetup employeeSetup { get; set; }
        public  PeopleRepresentative peopleRepresentative { get; set;}


    }
    public class MainModule
    {
        [Key()]
        public int Id { get; set; }
        [Display(Name = "शाखा")]
        public string Title { get; set; }
        [NotMapped]
        public List<MainModule> List { get;  set; }
    }

    public class SubModule {
        [Key()]
        public int Id { get; set; }
        [Display(Name = "सेवा")]
        public string Title { get; set; }
        [Display(Name = "शाखा")]
        public int? MainModuleId { get; set; }
        [ForeignKey("MainModuleId")]
        public virtual MainModule MainModule { get; set; }
        [Display(Name = "समय")]
        public string TimeNeed { get; set; }
        [Display(Name = "खर्च")]
        public string DasturNeed { get; set; }
        [Display(Name = "चाहिने कागजपत्र")]
        public string DocumentNeeded { get; set; }
        [Display(Name = "प्रकृया")]
        public string Process { get; set; }
        [NotMapped]
        public List<SubModule> List { get;  set; }
        public string FileName { get; internal set; }
        public string FilePath { get; internal set; }
    }

   
}
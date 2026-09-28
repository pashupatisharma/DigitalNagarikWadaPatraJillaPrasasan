using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class EmployeeSetup
    {
        [Key()]
        public int EmpId { get; set; }
        [Display(Name = "नाम")]
        public string EmpName { get; set; }
        [Display(Name = "नाम(अङ्ग्रेजीमा)")]
        public string EmpNameEng { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "सम्पर्क नम्बर")]
        public string Contact { get; set; }
        [Display(Name = "जन्म मिति")]
        public string DateOfBirth { get; set; }
        public DateTime? DateOfBirthEng { get; set; }
        [Display(Name = "नियुक्ति भएको मिति")]
        public string AppointmentDate { get; set; }
        public DateTime? AppointmentDateEng { get; set; }
        [Display(Name = "फोटो")]
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "पद")]
        public int? PostId { get; set; }
        public virtual PostSetup PostSetup { get; set; }    
        [Display(Name = "कर्मचारी प्रकार")]
        public int EmpTypeId { get; set; }
        public virtual EmployeeTypeSetup EmployeeTypeSetup { get; set; }
        [Display(Name = "लिङ्ग")]
        public int? Gender { get; set; }
        [Display(Name = "हेराउने क्रम")]
        public int? DisplayOrder { get; set; }

        [NotMapped()]
        public HttpPostedFileBase ImageFile { get; set; }
        [NotMapped()]
        public List<EmployeeSetup> Lists { get; set; }
    }
}
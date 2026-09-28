using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class EmployeeTypeSetup
    {
        [Key()]
        public int EmployeeTypeID { get; set; } 
        public string EmployeeTypeName {  get; set; }
        public string EmployeeTypeNameEng {  get; set; }
    }
}
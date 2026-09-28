using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Models
{
    public class HomeModel
    {
        internal bool isAcepted;

        public List<SelectListItem> officeList { get; set; }

        public List<SelectListItem> MedicineList { get; set; }
        public decimal? Quantity { get;  set; }
        public int? SubOffice_ID { get;  set; }
        public int ItemRecordId { get; set; }
    }
}
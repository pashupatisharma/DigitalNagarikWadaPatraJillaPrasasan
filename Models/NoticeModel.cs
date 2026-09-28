using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class NoticeModel
    {
        public int NoticeId { get; set; }

        public string DescriptionNotice { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }

        public string FileName { get; set; }
        public string CreateDate { get; set; }
     
        public List<NoticeModel> NoticeList { get; internal set; }
    }



}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class NoticeFiles
    {
        [Key]
        public int NoticeFilesId { get; set; }

        public string Image { get; set; }

        public string ImageUrl { get; set; }

        public int? NoticeId { get; set; }

        [ForeignKey("NoticeId")]
        public virtual Notice Notice { get; set; }

        [NotMapped]
        public List<NoticeFiles> lists { get; set; }
    }
}
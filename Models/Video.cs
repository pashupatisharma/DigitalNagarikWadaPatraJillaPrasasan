using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Video
    {
        [Key()]
        public int VideoId { get; set; }
        [Display(Name = "विडियोको नाम")]
        public string VideoName { get; set; }
        public string VideoUrl { get; set; }
        [Display(Name = "विडियो")]
        public string VideoData { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string UploadedBy { get; set; }

        [NotMapped()]
        public List<Video> Lists { get; set; }
    }
}
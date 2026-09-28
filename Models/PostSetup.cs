using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class PostSetup
    {
        [Key()] 
        public int PostId { get; set; } 
        public string PostTitle { get; set; }
        public string PostTitleEng { get; set; }

    }
}
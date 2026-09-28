using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class AppUserEntry
    {
        [Display(Name = "ठेगाना")]
        public string Address
        {
            get;
            set;
        }

        [Display(Name = "सम्पर्क  नम्बर")]
        public string ContactNo
        {
            get;
            set;
        }

        public int CounterId
        {
            get;
            set;
        }

        public int? CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }



        public int? EditedBy
        {
            get;
            set;
        }

        public DateTime? EditedDate
        {
            get;
            set;
        }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "ई–मेल ठेगाना")]
        public string EmailAddress
        {
            get;
            set;
        }

        [Display(Name = "पुरा नाम")]
        public string FullName
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public string MachineName
        {
            get;
            set;
        }

        public int OfficeId
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "पासवर्ड")]
        public string Password
        {
            get;
            set;
        }

        [Display(Name = "फोटो")]
        public string Photo
        {
            get;
            set;
        }

        [Display(Name = "जिम्मेवारी/पद")]
        public string Post
        {
            get;
            set;
        }
        [NotMapped()]
        public bool RememberMe
        {
            get;
            set;
        }
        [NotMapped()]
        public string RoleName
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        [Display(Name = "सब-कार्यालय")]
        [Required(ErrorMessage = "कृपया {0} राख्नुहोस्")]
        public int SubOfficeId
        {
            get;
            set;
        }

        [Display(Name = "संकेत नं(कर्मचारी)")]
        public string UserCodeNo
        {
            get;
            set;
        }

        [Display(Name = "प्रयोगकर्ता")]
        [RegularExpression("^\\S*$", ErrorMessage = "कृपया स्पेस नराख्नुहोस् ।")]
        [Required(ErrorMessage = "कृपया {0} राख्नुहोस्")]
        public string UserName
        {
            get;
            set;
        }

        public int UserRoleId
        {
            get;
            set;
        }
        public byte[] PhotoByte { get; internal set; }

        
    }
}
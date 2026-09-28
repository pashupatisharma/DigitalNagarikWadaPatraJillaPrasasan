using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HelpDesk.Models
{

    public class AppUserPassword
    {
        public virtual AppUser AppUser
        {
            get;
            set;
        }

        public int AppUserId
        {
            get;
            set;
        }

        public DateTime ChangedDate
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public bool IsReset
        {
            get;
            set;
        }

        public string NewPassword
        {
            get;
            set;
        }

        public string PreviousPassword
        {
            get;
            set;
        }

        public int? ResetBy
        {
            get;
            set;
        }

        public AppUserPassword()
        {
        }
    }
}


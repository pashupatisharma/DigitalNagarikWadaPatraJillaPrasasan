using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{

    public class UserRole
    {
        public virtual ICollection<AppUser> AppUsers
        {
            get;
            set;
        }

        [Key()]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public UserRole()
        {
            this.AppUsers = new HashSet<AppUser>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HelpDesk.Models
{
 
    public class AppUser
    {






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

    
        public int? DeletedBy
        {
            get;
            set;
        }
    
        public DateTime? DeletedDate
        {
            get;
            set;
        }


        public int? EditBy
        {
            get;
            set;
        }
 
        public DateTime? EditedDate
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




        public string Password
        {
            get;
            set;
        }

       




        public int Status
        {
            get;
            set;
        }



        


        public string UserName
        {
            get;
            set;
        }

        public virtual UserRole UserRole
        {
            get;
            set;
        }

        public int UserRoleId
        {
            get;
            set;
        }


      
        public virtual ICollection<AppUserPassword> AppUserPasswords
        {
            get;
            set;
        }
        [NotMapped]
        public string Name { get; set; }

        public byte[] PhotoByte { get; set; }
        [NotMapped]
        public string RoleName { get;  set; }

        public AppUser()
        {

            this.AppUserPasswords= new HashSet<AppUserPassword>();


        }
    }
}


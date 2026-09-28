using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpDesk.Models
{

    public class ApplicationUser : IdentityUser
    {
      

        public int Status { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Offices> Offices { get; set; }

        public DbSet<MainModule> MainModule { get; set; }

        public DbSet<SubModule> SubModule { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Notice> Notice { get; set; }
        public DbSet<PeopleRepresentative> PeopleRepresentative { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<PostSetup> PostSetup { get; set; }
        public DbSet<EmployeeSetup> EmployeeSetup { get; set; }
        public DbSet<EmployeeTypeSetup> EmployeeTypeSetup { get; set; }
        public DbSet<NoticeFiles> NoticeFiles { get; set; }
    }

















    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public int Status { get; set; }
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //    public DbSet<AppUser> AppUsers { get; set; }
    //    public DbSet<UserRole> UserRoles { get; set; }
    //    public DbSet<Offices> Offices { get; set; }

    //    public DbSet<MainModule> MainModule { get; set; }

    //    public DbSet<SubModule> SubModule { get; set; }
    //    public DbSet<Employee> Employee { get; set; }

    //    public DbSet<Notice> Notice { get; set; }
    //    public DbSet<PeopleRepresentative> PeopleRepresentative { get; set; }
    //    public DbSet<Video> Video { get; set; }
    //    public DbSet<PostSetup> PostSetup { get; set; }
    //    public DbSet<EmployeeSetup> EmployeeSetup { get; set; }
    //    public DbSet<EmployeeTypeSetup> EmployeeTypeSetup { get; set; }
    //    public DbSet<NoticeFiles> NoticeFiles { get;  set; }
    //}
}
namespace HelpDesk.Migrations
{
    using HelpDesk.Helper;
    using HelpDesk.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HelpDesk.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HelpDesk.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "SuperAdmin" };

                manager.Create(role);
            }

            IList<EmployeeTypeSetup> EmployeeTypeSetupList = new List<EmployeeTypeSetup>();
            EmployeeTypeSetupList.Add(new EmployeeTypeSetup() { EmployeeTypeID = 1, EmployeeTypeName = "जनप्रतिनिधि", EmployeeTypeNameEng = "Peoples' Representative" });
            EmployeeTypeSetupList.Add(new EmployeeTypeSetup() { EmployeeTypeID = 2, EmployeeTypeName = "कर्मचारी", EmployeeTypeNameEng = "Employee" });
            foreach (EmployeeTypeSetup weekDaySetUp in EmployeeTypeSetupList)
                context.EmployeeTypeSetup.AddOrUpdate(weekDaySetUp);


            context.MainModule.AddOrUpdate(new MainModule { Id = 1, Title = "आर्थिक प्रशासन शाखा" },
      new MainModule { Id = 2, Title = "न्यायीक समिति" },

            new MainModule { Id = 3, Title = "कृषि शाखा" },
                  new MainModule { Id = 4, Title = "जिन्सी शाखा" },
                        new MainModule { Id = 5, Title = "फंजीकरन् शाखा" },
                              new MainModule { Id = 6, Title = "पशु शाखा" },
                                    new MainModule { Id = 7, Title = "प्रशासन शाखा" },
                                          new MainModule { Id = 8, Title = "भवन तथा बस्ती बिकास शाखा" },
                                                new MainModule { Id = 9, Title = "महिला बालबालिका तथा समाज कल्याण उप-शाखा" },
                                                      new MainModule { Id = 10, Title = "योजना शाखा" },
                                                            new MainModule { Id = 11, Title = "राजश्व शाखा" },
                                                                  new MainModule { Id = 12, Title = "सामाजिक बिकास शाखा" },


       new MainModule { Id = 13, Title = "स्वास्थ्य शाखा" },
                                                            new MainModule { Id = 14, Title = "शिक्षा शाखा" },
                                                                  new MainModule { Id = 15, Title = "आर्थिक विकास शाखा" },

                                                                    new MainModule { Id = 16, Title = "जग्गा प्रशासन शाखा" }
                                                                  );



         

        }
    }
}

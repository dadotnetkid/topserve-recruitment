using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Data;

[assembly: OwinStartupAttribute(typeof(RecruitmentSystem.Startup))]
namespace RecruitmentSystem
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Seed();
        }
        public void Seed()
        {

            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (UserManager.FindByEmail("supervisor1@gmail.com") == null)
            {
                var user = new ApplicationUser
                                {
                                    UserName = "supervisor1",
                                    Email = "supervisor1@gmail.com",
                                };
                UserManager.Create(user, "hsgdev");
                UserManager.AddToRole(user.Id, "Recruitment Supervisor");
            }
            string AccountManager = "";
            if (UserManager.FindByEmail("accountmanager1@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "accountmanager1",
                    Email = "accountmanager1@gmail.com",
                };
                AccountManager = user.Id;
                UserManager.Create(user, "hsgdev");
                UserManager.AddToRole(user.Id, "Account Manager");
                new DatabaseModelDataContext().sp_register_user(user.Id, "accountmanager1", "accountmanager1", "accountmanager1", "male", "accountmanager1@gmail.com", "", null, null, null, null, null, null, "A034309B-E31A-4AE9-9628-04B4DD7193EB", "52e2e8a7-a3cc-40cb-976a-fc0d3b0d09e8");
            }
            if (UserManager.FindByEmail("coordinator1@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "coordinator1",
                    Email = "coordinator1@gmail.com",
                };
                UserManager.Create(user, "hsgdev");
                UserManager.AddToRole(user.Id, "Coordinator");
                new DatabaseModelDataContext().sp_register_user(user.Id, "coordinator1", "coordinator1", "coordinator1", "male", "coordinator1@gmail.com", "", null, null, null, AccountManager, AccountManager, null, "A034309B-E31A-4AE9-9628-04B4DD7193EB", "52e2e8a7-a3cc-40cb-976a-fc0d3b0d09e8");
            }
            if (UserManager.FindByEmail("recruiter1@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "recruiter1",
                    Email = "recruiter1@gmail.com",
                };
                UserManager.Create(user, "hsgdev");
                UserManager.AddToRole(user.Id, "Recruiter");
                new DatabaseModelDataContext().sp_register_user(user.Id, "recruiter1", "recruiter1", "recruiter1", "male", "recruiter1@gmail.com", "", null, null, null, null, null, null, "A034309B-E31A-4AE9-9628-04B4DD7193EB", "52e2e8a7-a3cc-40cb-976a-fc0d3b0d09e8");
            }
        }
    }
}

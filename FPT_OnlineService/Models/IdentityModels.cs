using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FPT_OnlineService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }

        [Column(Order=2)]
        public override string UserName { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<ApplicationUser>().ToTable("Users", "dbo");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo");
        modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
        modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
        modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.FormComment> FormComments { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.Semester> Semesters { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.Form> Forms { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.SuspendSemesterForm> SuspendSemesterForms { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.SuspendSubjectForm> SuspendSubjectForms { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.DropOutForm> DropOutForms { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.CourseRegForm> CourseRegForms { get; set; }

        public System.Data.Entity.DbSet<FPT_OnlineService.Models.RequestForm> RequestForms { get; set; }
    }
}
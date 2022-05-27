using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_Reviu_LKBUN.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string CompleteName { get; set; }
        public bool isAdmin { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<ActiveSession> ActiveSessions { get; set; }
        public DbSet<ST> STs { get; set; }
        public DbSet<Universe> Universes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<UniverseRisk> UniverseRisks { get; set; }
        public DbSet<RiskCategory> RiskCategories { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<OutputColumnList> OutputColumnLists{ get; set; }
        public DbSet<Assurance> Assurance { get; set; }
        public DbSet<AssuranceResult> AssuranceResult { get; set; }
        public DbSet<KPPN> KPPN { get; set; }
        public DbSet<Satker> Satker { get; set; }
        public DbSet<UniverseDetails> UniverseDetails { get; set; }
        public DbSet<Responses> Responses { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
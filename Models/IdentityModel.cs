using Microsoft.AspNet.Identity.EntityFramework;

namespace TelerivetExample.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool Has_accepted_policy { get; set; }
        public int user_type_id { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ABCSystemConnectionString")
        {

        }
    }
    //public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    //{
    //    //public ClaimsIdentity GenerateUserIdentity(CustomerManager manager)
    //    //{
    //    //    var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
    //    //    userIdentity.AddClaim(new Claim("Role", RoleManager.GetRole(Roles.First().RoleId).Name));
    //    //    userIdentity.AddClaim(new Claim("FullName", manager.FindById(Id).FullName));
    //    //    userIdentity.AddClaim(new Claim(ClaimTypes.Name, UserName));
    //    //    userIdentity.AddClaim(new Claim("RoleID", Roles.First().RoleId.ToString()));
    //    //    userIdentity.AddClaim(new Claim("UserID", Id.ToString()));
    //    //    return userIdentity;
    //    //}

    //    public Task<ClaimsIdentity> GenerateUserIdentityAsync(CustomerManager manager)
    //    {
    //        return Task.FromResult(GenerateUserIdentity(manager));
    //    }


    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string FullName { get; set; }
    //    public DateTime RegistrationDate { get; set; }
    //    public int Active { get; set; }
    //    //public string LocationID { get; set; }
    //    public int AddressID { get; set; }
    //    public string FaxNumber { get; set; }
    //    public string SSN { get; set; }
    //    public string CompanyName { get; set; }
    //    public int CreatedBy { get; set; }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    //int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    //{
    //    public ApplicationDbContext()
    //        : base(ConfigurationManager.ConnectionStrings["ABCSystemConnectionString"].ConnectionString)
    //    {
    //        Database.SetInitializer<IdentityDbContext>(new DropCreateDatabaseIfModelChanges<IdentityDbContext>());
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        modelBuilder.Entity<ApplicationUser>().ToTable("User").Property(p => p.Id).HasColumnName("UserID");
    //        modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
    //        modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
    //        modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
    //        modelBuilder.Entity<ApplicationUser>().Ignore(c => c.FullName);
    //        modelBuilder.Entity<CustomRole>().ToTable("Roles");

    //    }
    //}

    //public class CustomUserRole : IdentityUserRole<int> { }
    //public class CustomUserClaim : IdentityUserClaim<int> { }
    //public class CustomUserLogin : IdentityUserLogin<int> { }

    //public class CustomRole : IdentityRole<int, CustomUserRole>
    //{
    //    public CustomRole() { }
    //    public CustomRole(string name) { Name = name; }
    //}

    //public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
    //    CustomUserLogin, CustomUserRole, CustomUserClaim>
    //{
    //    public CustomUserStore(ApplicationDbContext context)
    //        : base(context)
    //    {
    //    }
    //}

    //public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    //{
    //    public CustomRoleStore(ApplicationDbContext context)
    //        : base(context)
    //    {
    //    }
    //}
}


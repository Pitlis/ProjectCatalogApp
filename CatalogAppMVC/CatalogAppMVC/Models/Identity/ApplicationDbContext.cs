using Microsoft.AspNet.Identity.EntityFramework;

namespace CatalogAppMVC.Models.Identity
{
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("CatalogConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
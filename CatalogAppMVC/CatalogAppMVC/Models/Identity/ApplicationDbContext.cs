using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogAppMVC.Models.Identity
{
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public ApplicationDbContext()
            : base("CatalogDatabaseConnectionString")
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}

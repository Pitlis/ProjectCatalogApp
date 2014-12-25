using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Identity
{
    internal class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {

        public CustomRoleStore(ApplicationDbContext context) : base(context)
        { }

    }
}
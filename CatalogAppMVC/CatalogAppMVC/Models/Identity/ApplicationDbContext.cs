﻿using CatalogAppMVC.App_Start.IdentityConfig;
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

        public void Initialize()
        {

            var manager = ApplicationUserManager.CreateStatic(this);

            //TODO: AplicationDbContext Добавить логику инициализации системных пользователей и групп.

        }

    }
}
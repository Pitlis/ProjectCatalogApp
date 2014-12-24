using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.Identity
{
    class ApplicationUser : IdentityUser, CatalogAppMVC.Models.interfaces.IUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public interfaces.IRole UserRole
        {
            //TODO: ApplicationUser сделать имплиментацию UserRole
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsActivated
        {
            get;
            set;
        }

        public string PathToPhoto
        {
            get;
            set;
        }

        public long Rating
        {
            get;
            set;
        }
    }
}

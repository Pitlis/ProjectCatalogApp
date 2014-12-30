using CatalogAppMVC.Models.interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.Identity
{
    public class ApplicationAuthentication : IMyAppAuthentication
    {

        IOwinContext _context;
        ApplicationUserManager _userManager;

        public ApplicationAuthentication(HttpContextBase context)
        {
            _context = context.GetOwinContext();
            _userManager = _context.GetUserManager<ApplicationUserManager>();
        }

        public int GetAuthenticationUserId()
        {
            string name = _context.Authentication.User.Identity.Name;
            var user = _userManager.FindByNameAsync(name).Result;

            return user.Id;
        }

        public string GetAuthenticationUserName()
        {
            return _context.Authentication.User.Identity.Name;
        }

        async public Task<int> GetAuthenticationUserRoleId()
        {
            string name = _context.Authentication.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);

            foreach (var role in user.Roles)
            {
                if (role.UserId == user.Id)
                {
                    return role.RoleId;
                }
            }

            throw new NullReferenceException("Could not find the user role.");
        }

        async public Task<long> GetAuthenticationUserRating()
        {
            string name = _context.Authentication.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);

            return user.Rating;
        }

    }
}
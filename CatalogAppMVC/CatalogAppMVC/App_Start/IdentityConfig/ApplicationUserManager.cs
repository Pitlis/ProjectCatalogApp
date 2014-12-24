using CatalogAppMVC.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Web;

namespace CatalogAppMVC.App_Start.IdentityConfig
{
    internal class ApplicationUserManager : UserManager<ApplicationUser>, CatalogAppMVC.Models.interfaces.IAuthorization
    {

        static IOwinContext _context;

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            _context = context;

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                //HACK: Отключил валидаию что бы не мучиться при тестировании.

                //RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            //HACK: Отключил блокировку пользователя.
            // Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });

            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        public static ApplicationUserManager CreateStatic(ApplicationDbContext context)
        {

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            return manager;

        }


        public string GetAuthorizUserId()
        {

            return _context.Authentication.User.Identity.GetUserId();

        }

        public Models.interfaces.IRole GetAuthorizUserRole()
        {
            
            var user_id = _context.Authentication.User.Identity.GetUserId();

            //TODO: GetAuthorizUserRole() Спрятать ApplicationUser за IUser
            ApplicationUser user = this.FindById(user_id);

            foreach (var role in user.Roles)
            {
                if (role.UserId == user_id)
                {
                    return (CatalogAppMVC.Models.interfaces.IRole)role;
                }
            }

            return null;

        }

        public long GetAuthorizUserRating()
        {

            var user_id = _context.Authentication.User.Identity.GetUserId();
            CatalogAppMVC.Models.interfaces.IUser user = this.FindById(user_id);

            return user.Rating;

        }

    }
}
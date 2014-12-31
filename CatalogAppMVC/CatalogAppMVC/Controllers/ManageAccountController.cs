using CatalogAppMVC.Models.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogAppMVC.Models;
using CatalogAppMVC.Models.WorkLinqToSql;
using CatalogAppMVC.Models.Identity.IdentityViewModel;
using System.Threading.Tasks;

namespace CatalogAppMVC.Controllers
{
    public class ManageAccountController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        internal ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        internal ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private CustomRoleStore _roleStore;

        private CustomRoleStore RoleStore
        {
            get
            {
                if (_roleStore == null)
                {
                    _roleStore = new CustomRoleStore(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
                    return _roleStore;
                }
                else
                {
                    return _roleStore;
                }
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetListUsers()
        {
            var listUsers = UserManager.Users.ToList<ApplicationUser>();

            if (listUsers != null)
            {
                List<ApplicationUser> sortListUser = new List<ApplicationUser>();

                foreach (var user in listUsers)
                {
                    if (user.IsActivated == false)
                    {
                        sortListUser.Add(user);
                    }
                }
                foreach (var user in listUsers)
                {
                    if (user.IsActivated == true)
                    {
                        sortListUser.Add(user);
                    }
                }

                UsersViewModel model = new UsersViewModel();
                model.Users = new List<UserViewModel>();

                for (int i = 0; i < sortListUser.Count; i++)
                {

                    UserViewModel userModel = new UserViewModel()
                    {
                        UserName = sortListUser[i].UserName,
                        Email = sortListUser[i].Email,                        
                        IsActivate = sortListUser[i].IsActivated
                    };

                    model.Users.Add(userModel);

                }

                return View(model);
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ActivateUser()
        {
            ActivateUserViewModel model = new ActivateUserViewModel();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ActivateUser(ActivateUserViewModel model)
        {
            if (model != null)
            {
                var _user = await UserManager.FindByEmailAsync(model.Email);

                if (_user != null)
                {
                    _user.IsActivated = true;
                    await UserManager.UpdateAsync(_user);
                    return RedirectToAction("GetListUsers");
                }
                else
                {
                    return View("_AppError");
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteUser()
        {
            DeleteUserViewModel model = new DeleteUserViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteUser(DeleteUserViewModel model)
        {
            if (model != null)
            {
                var _user = await UserManager.FindByEmailAsync(model.Email);

                if (_user == null)
                {
                    _user = await UserManager.FindByNameAsync(model.Name);
                }

                if (_user.Id != Access.ADMINID & _user.Id != Access.GUESTID)
                {
                    await UserManager.DeleteAsync(_user);
                    return RedirectToAction("GetListUsers");
                }
                else
                {
                    return View("_AppError");
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddUserToRole()
        {
            AddUserToRoleViewModel model = new AddUserToRoleViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
            if (model != null)
            {
                var _user = await UserManager.FindByEmailAsync(model.Email);

                if (_user != null)
                {
                    await UserManager.AddToRoleAsync(_user.Id, model.NameRole);
                    return RedirectToAction("GetListUsers");
                }
                else
                {
                    return View("_AppError");
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteUserFromRole()
        {
            AddUserToRoleViewModel model = new AddUserToRoleViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteUserFromRole(AddUserToRoleViewModel model)
        {
            if (model != null)
            {
                var _user = await UserManager.FindByEmailAsync(model.Email);

                if (_user != null)
                {
                    if (_user.Id != 1 & model.NameRole != "Administrator")
                    {
                        if (_user.Id != 2 & model.NameRole != "Guest")
                        {
                            await UserManager.AddToRoleAsync(_user.Id, model.NameRole);
                            return RedirectToAction("GetListUsers");
                        }
                    }

                    return View("_AppError");
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetListRoles()
        {
            var listRoles = RoleStore.Roles.ToList();
            
            if (listRoles != null)
            {

                RolesViewModel model = new RolesViewModel();
                model.Roles = new List<RoleViewModel>();

                foreach (var role in listRoles)
                {

                    RoleViewModel roleModel = new RoleViewModel() { Name = role.Name };
                    model.Roles.Add(roleModel);

                }

                return View(model);
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddRole()
        {
            AddRoleViewModel model = new AddRoleViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AddRole(AddRoleViewModel model)
        {
            if (model != null)
            {
                CustomRole _customRole = new CustomRole(model.Name);
                await RoleStore.CreateAsync(_customRole);
                return RedirectToAction("GetListRoles");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteRole()
        {
            AddRoleViewModel model = new AddRoleViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteRole(AddRoleViewModel model)
        {
            if (model != null)
            {
                var _role = await RoleStore.FindByNameAsync(model.Name);

                if (_role.Name != "Administrators" & _role.Id != 1)
                {
                    if (_role.Name != "Guest" & _role.Id != 2)
                    {
                        await RoleStore.DeleteAsync(_role);
                        return RedirectToAction("GetListRoles");
                    }
                }

                return View("_AppError");
            }

            return View("_AppError");
        }


        #region Category

        [Authorize(Roles = "Administrator")]
        public ActionResult ListCategory()
        {
            List<Category> listCategory;
            listCategory = Category.GetAllCategory();
            return View(listCategory);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory(Category categoryNew)
        {
            categoryNew.AddToBase();
            return RedirectToAction("ListCategory");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category.DeleteCategory(categoryId);
            return RedirectToAction("ListCategory");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCategory(int categoryId)
        {
            return View(Category.GetCategory(categoryId));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCategory(Category categoryNew)
        {
            Category.EditCategory(categoryNew);
            return RedirectToAction("ListCategory");
        }

        #endregion
    }
}
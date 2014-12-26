using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Identity.IdentityViewModel
{
    public class LoginViewModel
    {

        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember Me?")]
        public bool RememberMe { get; set; }

    }
}
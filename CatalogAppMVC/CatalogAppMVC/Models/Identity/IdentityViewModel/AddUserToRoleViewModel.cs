using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Identity.IdentityViewModel
{
    public class AddUserToRoleViewModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string NameRole { get; set; }

    }
}
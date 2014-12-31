using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Identity.IdentityViewModel
{
    public class RoleViewModel
    {

        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

    }
}
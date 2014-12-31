using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CatalogAppMVC.Models.Identity.IdentityViewModel
{
    public class ActivateUserViewModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Is Activate")]
        public bool IsActivate { get; set; }

    }
}
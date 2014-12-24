using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IUser
    {

        string Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string PasswordHash { get; set; }

        CatalogAppMVC.Models.interfaces.IRole UserRole { get; set; }

        bool IsActivated { get; set; }

        string PathToPhoto { get; set; }

        long Rating { get; set; }

    }
}

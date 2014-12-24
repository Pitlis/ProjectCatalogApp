using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IUser
    {

        string Id { get; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string PhoneNumber { get; set; }

        ICollection<IRole> Roles { get; }

        long Rating { get; set; }

    }
}

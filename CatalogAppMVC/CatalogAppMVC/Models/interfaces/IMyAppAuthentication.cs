using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IMyAppAuthentication
    {

        int GetAuthenticationUserId();

        string GetAuthenticationUserName();

        int GetAuthenticationUserRoleId();

        long GetAuthenticationUserRating();

    }
}

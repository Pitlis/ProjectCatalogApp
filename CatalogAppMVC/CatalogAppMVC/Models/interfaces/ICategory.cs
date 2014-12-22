using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface ICategory
    {
        //возвращает список категорий, в которые может писать данный пользователь
        List<Category> GetCategoriesForWrite(IUser user);
        List<Category> GetOpenCategory(IUser user);
    }
}

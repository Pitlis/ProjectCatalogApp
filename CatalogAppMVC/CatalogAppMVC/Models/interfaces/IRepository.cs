using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IRepository
    {
        bool CreateRecord(Record record);
        CatalogAppMVC.Models.Record GetRecord(int RecordID);
        CatalogAppMVC.Models.File GetFile(int fileID);

    }
}

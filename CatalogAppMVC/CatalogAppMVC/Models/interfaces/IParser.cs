using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parser_infofrezer_ru
{
    public interface IParser
    {
        Dictionary<string, string> GetCategories(string categoriesURL);
        Dictionary<string, string> GetRecordsFromCategory(string categoryURL);
        IRecord GetRecord(string recordURL);
    }
}

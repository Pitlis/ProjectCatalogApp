using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parser_infofrezer_ru
{
    interface IRecord
    {
        string Name { get; set; }
        string Description { get; set; }
        string PhotoHeadUrl { get; set; }
        Dictionary<string, string> Specifications { get; set; }
        Dictionary<string, string> Files { get; set; }
    }
}

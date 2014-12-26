using ParserInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parser_infofrezer_ru
{
    class RecordParser: IRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Specifications { get; set; }
        public Dictionary<string, string> Files { get; set; }
        public string PhotoHeadUrl { get; set; }
    }
}

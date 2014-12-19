using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class File
    {
        public string AuthorName { get; private set; }
        public string Name { get; private set; }
        public double Size { get; private set; }
        public string type { get; private set; }
        public string typeName { get; private set; }

        public string PachToFile;

        public File(string AName, string name, double size, string typename, string filetype, string pachToFile)
        {
            AuthorName = AName;
            Name = name;
            Size = size;
            typeName = typename;
            type = filetype;
            PachToFile = pachToFile;
        }
    }
}
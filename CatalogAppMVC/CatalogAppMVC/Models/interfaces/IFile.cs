﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CatalogAppMVC.Models.interfaces
{
    interface IFile
    {
        string GetPatchToFile();
        string GetFileName();
    }
}
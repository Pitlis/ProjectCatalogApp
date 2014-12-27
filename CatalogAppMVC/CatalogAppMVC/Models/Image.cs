using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public static class Image
    {
        public const string IMAGEHEAD = "ImageHead"; //тип документа для аватарки записи
        public const string IMAGE = "Image"; //тип документа для изображений

        public static string GetHeadImageFromFiles(IEnumerable<File> files)
        {
            //TODO Использовать HttpHandler для сокрытия полного пути
            foreach (File file in files)
            {
                if (file.DocumentType == IMAGEHEAD)
                    return file.GetPatchToFile();
            }
            return ConfigurationManager.AppSettings["DefaultImageForHead"] as string;
        }

    }
}
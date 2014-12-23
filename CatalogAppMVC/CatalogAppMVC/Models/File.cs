using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class File
    {
        public const string TYPEFILES = "multipart/form-data";

        public int FileID { get; private set; }
        public string AuthorName { get; private set; }
        public string DocumentName { get; private set; }
        public string DocumentType { get; private set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public double Size { get; private set; }

        public string PachToFile;//путь к файлу относительно каталога, где хранятся файлы

        public File(int ID, string authorName, string documentName, string documentType, string fileName, string fileType, double size, string pachToFile)
        {
            FileID = ID;
            AuthorName = authorName;
            DocumentName = documentName;
            DocumentType = documentType;
            FileName = fileName;
            FileType = fileType;
            Size = size;
            PachToFile = pachToFile;
        }


        public string GetPatchToFile()
        {
            string filesDirectory = ConfigurationManager.AppSettings["FilesDirectory"] as string;
            return filesDirectory + PachToFile + FileName + "." + FileType;
        }
        public string GetFileName()
        {
            return FileName + "." + FileType;
        }



        //Методы для работы с БД



    }
}
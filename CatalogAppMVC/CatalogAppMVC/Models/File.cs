using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class File
    {
        public const string TYPEFILES = "multipart/form-data";

        public int FileID { get; private set; }
        public int AuthorName { get; private set; }
        public string DocumentName { get; private set; }
        public string DocumentType { get; private set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public double Size { get; private set; }
        public int RecordID { get; set; }

        public string PachToFile;//путь к файлу относительно каталога, где хранятся файлы

        public File(int ID, int authorName, string documentName, string documentType, string fileName, string fileType, double size, string pachToFile)
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

        public static File CreateFileLink(string fileURL, string fileName, string pachToFile)
        {
            WebClient webclient = new WebClient();
            string ext = Path.GetExtension(fileURL);
            string name = Path.GetFileNameWithoutExtension(fileURL);
            string filesDirectory = ConfigurationManager.AppSettings["FilesDirectory"] as string;
            File file = new File(0, 1, fileName, ext, name, ext, 0, pachToFile);

            try
            {

                webclient.DownloadFile(new Uri(fileURL), HttpContext.Current.Server.MapPath(@filesDirectory) + pachToFile + name);
            }
            catch
            {
                return null;
            }
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(@filesDirectory) + pachToFile + name);
            file.Size = fileInfo.Length;

            return file;
        }

        //Методы для работы с БД

        public void AddToBase()
        {
            IRepository repository = new Repository();
            repository.CreateFile(this);
        }

    }
}
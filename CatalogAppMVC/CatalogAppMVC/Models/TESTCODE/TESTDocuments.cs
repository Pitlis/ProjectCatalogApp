using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CatalogAppMVC.Models.TESTCODE
{
    public static class TESTDocuments
    {
        public static List<File> GetFilesTEST()
        {
            List<File> list = new List<File>();
            list.Add(new File(1, 1, "Какой-то чертеж1", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(2, 1, "Какой-то чертеж2", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(3, 1, "Какой-то чертеж3", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(4, 1, "Какой-то чертеж4", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(5, 1, "Какой-то чертеж5", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(6, 1, "Большая png", Image.IMAGE, "img1", "png", 1.3, "/Images/"));
            list.Add(new File(7, 1, "Средняя jpg", Image.IMAGE, "img2", "jpg", 1.3, "/Images/"));
            list.Add(new File(8, 1, "маленькая gif", Image.IMAGE, "img3", "gif", 1.3, "/Images/"));
            return list;
        }

        public static File GetFile(int fileID)
        {
            List<File> files = GetFilesTEST();
            //TODO добавить в класс репозитория метод получения файла
            return files[fileID - 1];
        }
    }
}
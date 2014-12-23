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
            list.Add(new File(1, "Вася Пупкин1", "Какой-то чертеж1", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(2, "Вася Пупкин2", "Какой-то чертеж2", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(3, "Вася Пупкин3", "Какой-то чертеж3", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(4, "Вася Пупкин4", "Какой-то чертеж4", "чертеж", "testFile", "pdf", 1.3, "/"));
            list.Add(new File(5, "Вася Пупкин5", "Какой-то чертеж5", "чертеж", "testFile", "pdf", 1.3, "/"));
            return list;
        }

        public static File GetFile(int fileID)
        {
            //TODO добавить в класс репозитория метод получения файла
            return new File(1, "Вася Пупкин1", "Какой-то чертеж1", "чертеж", "testFile", "pdf", 1.3, "/");
        }
    }
}
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
            list.Add(new File("Вася Пупкин", "Вид общий", 1.2, "чертеж", "application/pdf", "testFile.pdf"));
            list.Add(new File("Вася Пупкин", "Вид общий2", 1.2, "чертеж", "application/pdf", "testFile.pdf"));
            list.Add(new File("Вася Пупкин", "Вид общий3", 1.2, "документ", "application/pdf", "testFile.pdf"));
            list.Add(new File("Вася Васичкин", "Вид общий4", 1.2, "чертеж", "application/pdf", "testFile.pdf"));
            list.Add(new File("Вася Пупкин", "Вид общий5", 1.2, "книга", "application/pdf", "testFile.pdf"));
            return list;
        }
    }
}
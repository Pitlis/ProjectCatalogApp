using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace CatalogAppMVC.Models
{
    public class Tag
    {
        public string Name { get; set; }

        public static List<Tag> CreateTagsFromString(string str)
        {
            List<Tag> tags = new List<Tag>();

            str = Regex.Replace(str, "[^A-ZА-Яa-zа-я,]", "");
            str = Regex.Replace(str, ",{2,}", ",");
            String[] tagsString = str.Split(',');
            foreach (string s in tagsString)
            {
                if (s.Length > 0)
                {
                    tags.Add(new Tag() { Name = s });
                }
            }

            return tags;
        }
    }
}
using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.TESTCODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class Record
    {
        public enum Status { PREMODERATION, APPROVED, DECLINED }

        public int ID {get;  set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Specification> Specifications { get; set; }
        public List<File> Files { get; set; }
        public int CategoryID { get; set; }
        public int UserAuthorID { get; set; }
        private Status _status;

        public Record(IMyAppAuthentication user, int categoryID)
        {
            CategoryID = categoryID;
            UserAuthorID = user.GetAuthenticationUserId();
            Specifications = TestISpecification.GetMandatSpecifications(CategoryID);
        }
        public Record()
        {
            CategoryID = 0;
            UserAuthorID = 0;
        }
        


        public void LoadFromPage(Record recordFromPage)
        {
            for (int i = 0; i < Specifications.Count; ++i)
            {
                Specifications[i].Value = recordFromPage.Specifications[i].Value;
            }
            Name = recordFromPage.Name;
            Description = recordFromPage.Description;
        }

        public void LoadTagsFromString(string tagsString)
        {
            Tags = Tag.CreateTagsFromString(tagsString);
        }




        //Методы для работы с БД

        public static Record GetRecord(int recordID)
        {
            throw new NotImplementedException();
        }
        public static List<Record> GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public void AddToDataBase()
        {
            IRepository repository = new Repository();
            repository.CreateMachinery(this);
        }
    }
}
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
        public enum StatusType { PREMODERATION, APPROVED, DECLINED }

        public int ID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Specification> Specifications { get; set; }
        public List<File> Files { get; set; }
        public int CategoryID { get; set; }
        public int UserAuthorID { get; set; }
        public StatusType Status { get; private set; }

        public Record(IMyAppAuthentication user, int categoryID)
        {
            ID = 0;
            CategoryID = categoryID;
            UserAuthorID = user.GetAuthenticationUserId();
            Specifications = TestISpecification.GetMandatSpecifications(CategoryID);
        }
        public Record()
        {
            ID = 0;
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
            IRepository repository = new Repository();
            Record record = null;
            try
            {
                WorkLinqToSql.Machinery mach = (from m in repository.Machinerys where m.Id == recordID select m).Single<WorkLinqToSql.Machinery>();
                record = repository.ToRecord(mach);
            }
            catch
            {
                return null;
            }
            return record;
        }

        public static List<Record> GetAllRecords()
        {
            IRepository repository = new Repository();
            List<Record> list = new List<Record>();
            var recordsFromBase = from r in repository.Machinerys select r;
            foreach (WorkLinqToSql.Machinery machinery in recordsFromBase)
            {
                list.Add(repository.ToRecord(machinery));
            }
            return list;
        }

        public void AddToDataBase()
        {
            IRepository repository = new Repository();
            ID = repository.CreateMachinery(this);
        }

        public void ChangeStatus(StatusType status)
        {
            if(ID != 0) //Id == 0 когда запись создана, но еще не добавлена в базу
            {
                IRepository repository = new Repository();
                if (repository.UpdateStatusMachinery(status, ID))
                {
                    Status = status;
                }
            }
        }

        public void SetID(int id)
        {
            ID = id;
        }
    }
}
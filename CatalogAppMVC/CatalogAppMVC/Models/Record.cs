using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    enum Status {moder, good}
    public class Record
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; private set; }
        public List<Specification> Specifications { get; set; }

        private IUser _userAuthor;
        private Status _status;
        private int _categoryID;
        private List<File> _files;

        public Record(IUser user, int categoryID, ISpecification specification)
        {
            _categoryID = categoryID;
            _userAuthor = user;
            Specifications = specification.GetMandatSpecifications(_categoryID);
        }
        
        public Record()
        {
            ;
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
    }
}
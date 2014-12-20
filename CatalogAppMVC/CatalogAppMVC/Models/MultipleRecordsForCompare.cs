using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class MultipleRecordsForCompare
    {
        Dictionary<string, List<string>> _specifications = new Dictionary<string,List<string>>();

        public List<string> RecordNames { get; private set; }
        //public List<int> RecordImages { get; private set; }

        public MultipleRecordsForCompare(IEnumerable<Record> records)
        {
            RecordNames = new List<string>();
            foreach(Record record in records)
            {
                RecordNames.Add(record.Name);
                foreach(Specification sp in record.Specifications)
                {
                    if (!_specifications.ContainsKey(sp.Name))
                    {
                        _specifications.Add(sp.Name, new List<string>());
                    }
                }
            }

            foreach(KeyValuePair<string, List<string>> kv in _specifications)
            {
                foreach (Record record in records)
                {
                    bool foundValueSp = false;
                    foreach (Specification sp in record.Specifications)
                    {
                        if (sp.Name.Equals(kv.Key))
                        {
                            _specifications[kv.Key].Add(sp.Value);
                            foundValueSp = true;
                            break;
                        }
                    }
                    if (!foundValueSp)
                    {
                        _specifications[kv.Key].Add(null);
                    }
                }
            }



        }

        public Dictionary<string, List<string>> GetSpecifications()
        {
            return _specifications;
        }
    }
}
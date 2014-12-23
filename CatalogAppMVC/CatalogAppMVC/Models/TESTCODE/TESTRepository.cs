using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.TESTCODE
{
    public class TESTRepository: CatalogAppMVC.Models.interfaces.IRepository
    {
        public bool CreateMachinery(Machinery instance)
        {
            throw new NotImplementedException();
        }

        public File GetFile(int fileID)
        {
            //TODO добавить в класс репозитория метод получения файла
            return new File(1, "Вася Пупкин1", "Какой-то чертеж1", "чертеж", "testFile", "pdf", 1.3, "/");
        }

        public CatalogAppMVC.Models.Record GetRecord(int RecordID)
        {
            //TODO добавить в класс репозитория метод получения записи
            //Все ниже - заглушка!!!
            CatalogAppMVC.Models.interfaces.ISpecification specification = new TestISpecification();
            Record record = new Record() { ID = 1, Name = "Test Record", Description = "Помните такой телефон — Nokia 3310? Разумеется, помните! А такую штуку как синтезатор мелодий в нем? Тоже помните, отлично. А по старым, теплым и ламповым мелодиям скучаете? Вот и я скучаю. А еще мне на глаза попался сайтик с более чем сотней нотных листов для этого редактора. И что я должен был оставить эту прелесть без внимания? Нет уж. Что я сделал? Правильно! Взял и написал точно такой же генератор мелодий, который позволяет на выходе получить" };
            record.Specifications = specification.GetMandatSpecifications(1);
            record.Files = TESTDocuments.GetFilesTEST();
            record.Tags = Tag.CreateTagsFromString("test1, test2, test3, test4");
            record.Files = TESTDocuments.GetFilesTEST();
            return record;
        }


        IQueryable<LinqToSqlMdl.CatalogCategories> interfaces.IRepository.CatalogCategories
        {
            get { throw new NotImplementedException(); }
        }

        bool interfaces.IRepository.CreateCatalogCategories(LinqToSqlMdl.CatalogCategories instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.UpdateCatalogCategories(LinqToSqlMdl.CatalogCategories instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.RemoveCatalogCategories(int idCatalogCategories)
        {
            throw new NotImplementedException();
        }

        IQueryable<LinqToSqlMdl.Machinery> interfaces.IRepository.Machinerys
        {
            get { throw new NotImplementedException(); }
        }

        bool interfaces.IRepository.CreateMachinery(LinqToSqlMdl.Machinery instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.UpdateMachinery(LinqToSqlMdl.Machinery instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.RemoveMachinery(int idMachinery)
        {
            throw new NotImplementedException();
        }

        IQueryable<LinqToSqlMdl.Specifications> interfaces.IRepository.Specifications
        {
            get { throw new NotImplementedException(); }
        }

        bool interfaces.IRepository.CreateSpecifications(LinqToSqlMdl.Specifications instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.UpdateSpecifications(LinqToSqlMdl.Specifications instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.RemoveSpecifications(int idSpecifications)
        {
            throw new NotImplementedException();
        }

        IQueryable<LinqToSqlMdl.Tags> interfaces.IRepository.Tags
        {
            get { throw new NotImplementedException(); }
        }

        bool interfaces.IRepository.CreateTags(LinqToSqlMdl.Tags instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.UpdateTags(LinqToSqlMdl.Tags instance)
        {
            throw new NotImplementedException();
        }

        bool interfaces.IRepository.RemoveTags(int idTags)
        {
            throw new NotImplementedException();
        }
    }
}
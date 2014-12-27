using HtmlAgilityPack;
using ParserInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parser_infofrezer_ru
{
    class Parser: IParser
    {
        const string SITE = "http://infofrezer.ru";

        Dictionary<string, string> IParser.GetCategories()
        {
            HtmlDocument html = GetHtmlDocument(SITE + "/catalog");
            HtmlNodeCollection categoryNodes = html.DocumentNode.SelectNodes("//div[contains(@class,'category')]/div[@class='spacer']/h2/a");

            Dictionary<string, string> categories = new Dictionary<string, string>();

            try
            {
                foreach (HtmlNode category in categoryNodes)
                {
                    string name = category.Attributes["title"].Value;
                    string url = SITE + category.Attributes["href"].Value;
                    categories.Add(name, url);
                }
            }
            catch
            {
                throw new Exception("Невозможно получить список каталогов - " + SITE + "/catalog");
            }
            return categories;
        }
        Dictionary<string, string> IParser.GetRecordsFromCategory(string categoryURL)
        {
            HtmlDocument html = GetHtmlDocument(categoryURL);
            HtmlNodeCollection recordNodes = html.DocumentNode.SelectNodes("//div[contains(@class,'product')]/div[@class='spacer']/div/a");

            Dictionary<string, string> records = new Dictionary<string, string>();
            try
            {
                foreach (HtmlNode record in recordNodes)
                {
                    string name = record.Attributes["title"].Value;
                    string url = SITE + record.Attributes["href"].Value;
                    records.Add(name, url);
                }
            }
            catch
            {
                throw new Exception("Невозможно получить список записей в разделе каталога - " + categoryURL);
            }
            return records;
        }
        IRecord IParser.GetRecord(string recordURL)
        {
            HtmlDocument html = GetHtmlDocument(recordURL);

            IRecord record = new RecordParser();

            //Обязательные составляющие записи
            try { record.Name = GetName(html.DocumentNode); }
            catch { throw new Exception("Невозможно получить имя записи - " + recordURL); }

            try { record.Description = GetDescription(html.DocumentNode); }
            catch { throw new Exception("Невозможно получить описание записи - " + recordURL); }

            try { record.Specifications = GetSpecifications(html.DocumentNode); }
            catch { throw new Exception("Невозможно получить спецификации - " + recordURL); }
            //----

            record.Files = GetFiles(html.DocumentNode);
            record.PhotoHeadUrl = GetPhoto(html.DocumentNode);

            return record;
        }
                #region For GetRecord()

        string GetName(HtmlNode documentNode)
        {
            return documentNode.SelectSingleNode("//meta[@name='title']").Attributes["content"].Value;
        }
        string GetDescription(HtmlNode documentNode)
        {
            HtmlNode descriptionNode = documentNode.SelectSingleNode("//div[@class='product-description']//dl[@id='tabs']");
            return GetDdNode(GetDtNodeWithText(descriptionNode, "Описание", "span/h3/a")).SelectSingleNode("//p").InnerText;
        }
        string GetPhoto(HtmlNode documentNode)
        {
            string photoURL = null;
            try
            {
                photoURL = documentNode.SelectSingleNode("//div[@class='main-image']/a").Attributes["href"].Value;
            }
            catch { }
            return photoURL;
        }
        Dictionary<string, string> GetSpecifications(HtmlNode documentNode)
        {
            Dictionary<string, string> specifications = new Dictionary<string, string>();
            HtmlNode descriptionNode = documentNode.SelectSingleNode("//div[@class='product-description']//dl[@id='tabs']");

            foreach (HtmlNode tr in GetDdNode(GetDtNodeWithText(descriptionNode, "Характеристики", "span/h3/a")).SelectNodes("table/tbody/tr"))
            {
                HtmlNodeCollection twoTD = tr.SelectNodes("td");

                string specName = twoTD[0].InnerText;
                string specValue = twoTD[1].InnerText;

                specifications.Add(specName, specValue);
            }

            return specifications;
        }
        Dictionary<string, string> GetFiles(HtmlNode documentNode)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();
            HtmlNode descriptionNode = documentNode.SelectSingleNode("//div[@class='product-description']//dl[@id='tabs']");
            try
            {
                foreach (HtmlNode anchor in GetDdNode(GetDtNodeWithText(descriptionNode, "Програмное обеспечение", "span/h3/a")).SelectNodes("p/a"))
                {
                    string fileName = anchor.Attributes["title"].Value;
                    string fileURL = SITE + anchor.Attributes["href"].Value;

                    files.Add(fileName, fileURL);
                }
            }
            catch 
            {
                files = null;
            }

            return files;
        }



        static HtmlNode GetDdNode(HtmlNode dtNode)
        {
            HtmlNode found = dtNode.SelectSingleNode("following-sibling::*[1][self::dd]");
            return found;
        }
        static HtmlNode GetDtNodeWithText(HtmlNode dlNode, string text, string tags)
        {
            foreach (HtmlNode node in dlNode.SelectNodes("dt"))
            {
                if (node.SelectSingleNode(tags) != null && node.SelectSingleNode(tags).InnerText == text)
                {
                    return node;
                }
            }
            return null;
        }

        #endregion




        static HtmlDocument GetHtmlDocument(string url)
        {
            HtmlDocument hd = new HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            try
            {
                hd = web.Load(url);
            }
            catch
            {
                throw new Exception("Нет доступа к странице - " + url);
            }
            return hd;
        }

    }
}

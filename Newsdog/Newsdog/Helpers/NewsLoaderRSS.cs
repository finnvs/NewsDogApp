using System.Collections.Generic;
using System.Xml;
using Newsdog.Models;

namespace Newsdog.Helpers
{
    public class NewsLoaderRSS // : INewsLoader
    {
        public string Url { get; set; }
        public NewsLoaderRSS(string url)
        {
            Url = url;
        }

        public List<SimpleNewsInfo> GetSimpleNews()
        {
            List<SimpleNewsInfo> newsList = new List<SimpleNewsInfo>();
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(Url);
            XmlNodeList items = xmldoc.SelectNodes("//item");
            var xmlnamespacemanager = new XmlNamespaceManager(xmldoc.NameTable);
            xmlnamespacemanager.AddNamespace("DR", "http://dr.dk/channel/2.0/modules/dr/");
            foreach (XmlNode item in items)
            {
                XmlNode title = item.SelectSingleNode("title");
                XmlNode description = item.SelectSingleNode("description");
                XmlNode link = item.SelectSingleNode("link");


                XmlNode img = item.SelectSingleNode("DR:XmlImageArticle", xmlnamespacemanager).FirstChild;

                newsList.Add(CreateSimpleNewsInfo(title, description, link, img));
            }

            SimpleNewsInfo CreateSimpleNewsInfo(XmlNode title, XmlNode description, XmlNode link, XmlNode img)
            {
                return new SimpleNewsInfo(title.InnerText, description.InnerText, link.InnerText, img.InnerText);
            }

            return newsList;
        }   


    }
}

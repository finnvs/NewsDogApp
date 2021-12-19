using System;
using System.Collections.Generic;
using System.Xml;
using Newsdog.Models;

namespace Newsdog.Helpers
{
    public class NewsLoaderRSS // : INewsLoader
    {
        public enum FeedLogoImage
        {
            DrDk,
            Bbc,
            NyTimes,
            Buzzfeed,
            Cnn
        }
        // private backing field
        private static FeedLogoImage currentFeedLogo;
        // public property              
        public static FeedLogoImage CurrentFeedLogo
        {
            get { return currentFeedLogo; }
            set { currentFeedLogo = value; }
        }
        
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

            // makeum thumbnail image xml node object just to process in foreach loop
            XmlNode logoImage = xmldoc.CreateElement("LogoImage");
            // .. and now switch image url to match. TODO: change namespace here as well
            switch (CurrentFeedLogo)
            {
                case FeedLogoImage.DrDk:
                    logoImage.InnerText = @"https://is1-ssl.mzstatic.com/image/thumb/Purple124/v4/29/55/2e/29552ea2-5952-af7a-f398-89d177968258/AppIcon-0-0-1x_U007emarketing-0-0-0-7-0-0-85-220.png/600x600wa.png";
                    break;
                case FeedLogoImage.Bbc:
                    logoImage.InnerText = @"https://news.bbcimg.co.uk/nol/shared/img/bbc_news_120x60.gif";
                    break;
                case FeedLogoImage.NyTimes:
                    logoImage.InnerText = @"https://i1.feedspot.com/4719130.jpg?t=1603890044";
                    break;
                case FeedLogoImage.Buzzfeed:
                    logoImage.InnerText = @"https://i1.feedspot.com/4436201.jpg?t=1615026116";
                    break;
                case FeedLogoImage.Cnn:
                    logoImage.InnerText = @"https://i1.feedspot.com/30696.jpg?t=1612613253";
                    break;
                // default: set dr dk logo for news items displayed in mobile feed
                default:
                    logoImage.InnerText = @"https://is1-ssl.mzstatic.com/image/thumb/Purple124/v4/29/55/2e/29552ea2-5952-af7a-f398-89d177968258/AppIcon-0-0-1x_U007emarketing-0-0-0-7-0-0-85-220.png/600x600wa.png";
                    break;
            }          

            foreach (XmlNode item in items)
            {
                XmlNode title = item.SelectSingleNode("title");
                // Select på publish date istedet hvis desc er null (tilfældet for DR DK's nuværende XML feed)
                XmlNode description = item.SelectSingleNode("description") ?? item.SelectSingleNode("pubDate");                
                XmlNode link = item.SelectSingleNode("link");
                // Image xml noden er lig null i både DR og BBC's nye xml feed, så her er DR's logo i venstre spalte
                XmlNode img = item.SelectSingleNode("image") ?? logoImage;
                XmlNode pubDate = item.SelectSingleNode("pubDate");
                newsList.Add(CreateSimpleNewsInfo(title, description, link, img, pubDate));
            }

            SimpleNewsInfo CreateSimpleNewsInfo(XmlNode title, XmlNode description, XmlNode link, XmlNode img, XmlNode pubDate)
            { 
                try 
                { 
                    return new SimpleNewsInfo(title.InnerText, description.InnerText, link.InnerText, img.InnerText, pubDate.InnerText);
            }
                catch (Exception ex)
            {
                    // log error og stacktrace, output strings in any case
                    string msg = ex.Message;
                    string stacktrace = ex.StackTrace;
                    return new SimpleNewsInfo("Error", ex.Message,"Error", img.InnerText, "Error");
                }
            }

            return newsList;
        }   


    }
}

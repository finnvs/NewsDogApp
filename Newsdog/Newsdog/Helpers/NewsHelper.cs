using Newtonsoft.Json;
using Newsdog.News;
using Newsdog.News.Trending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using Newsdog.Models;

namespace Newsdog.Helpers
{
    public static class NewsHelper
    {
        public static async Task<List<NewsInformation>> GetByCategoryAsync(NewsCategoryType category)
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"https://api.cognitive.microsoft.com/bing/v5.0/news/?Category={category}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<NewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description = item.description,
                           CreatedDate = item.datePublished,
                           ImageUrl = item.image?.thumbnail?.contentUrl,

                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();
        }

        public static async Task<List<NewsInformation>> GetAsync(string searchQuery)
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/news/search?q={searchQuery}&count=10&offset=0&mkt=en-us&safeSearch=Moderate";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<NewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description = item.description,
                           CreatedDate = item.datePublished,
                           ImageUrl = item.image?.thumbnail?.contentUrl,

                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();
        }

        public async static Task<List<NewsInformation>> Bing_GetTrendingAsync()
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"https://api.cognitive.microsoft.com/bing/v7.0/news/trendingtopics";
            // string searchUrl = $"https://www.dr.dk/nyheder/service/feeds/allenyheder";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);

            var newsResult = JsonConvert.DeserializeObject<TrendingNewsResult>(result);
            // var newsResult = new XMLHelper.Deserialize<TrendingNewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description = item.query.text,
                           CreatedDate = DateTime.Now,
                           ImageUrl = item.image.url,

                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();

        }

        // Async løsning
        public async static Task<List<NewsInformation>> GetTrendingAsync()
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"http://www.dr.dk/nyheder/service/feeds/allenyheder";
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(searchUrl);
            SyndicationFeed feed = await Task.Run(() => SyndicationFeed.Load(reader));
            reader.Close();


            results = (from item in feed.Items
                       select new NewsInformation()
                       {
                           Title = item.Title.Text,
                           Description = item.Summary.Text,
                           CreatedDate = item.PublishDate.DateTime,
                           ImageUrl = @"https://is1-ssl.mzstatic.com/image/thumb/Purple124/v4/29/55/2e/29552ea2-5952-af7a-f398-89d177968258/AppIcon-0-0-1x_U007emarketing-0-0-0-7-0-0-85-220.png/600x600wa.png"
                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();
            //return results.Where(w => string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();
        }

        // Sync løsning
        public static List<SimpleNewsInfo> GetSimpleNews()
        {
            var DR_RSS = new NewsLoaderRSS($"http://www.dr.dk/nyheder/service/feeds/allenyheder");
            return DR_RSS.GetSimpleNews();
        }
    }
}

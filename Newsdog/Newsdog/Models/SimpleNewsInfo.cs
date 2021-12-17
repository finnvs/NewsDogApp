﻿namespace Newsdog.Models
{
    public class SimpleNewsInfo {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }        
        public string ImageUrl { get; set; }
        public string PubDate { get; set; }

        public SimpleNewsInfo(string title, string description, string link, string img, string pubDate)
        {
            Title = title;
            Description = description; 
            Link = link; 
            ImageUrl = img;
            PubDate = pubDate;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Occupie.Models;
using System.Xml;
using System.ServiceModel.Syndication;

namespace Occupie.RSSreader
{
    public class Reader
    {
        public List<BlogPost> RSS()
        {
            string url = "http://occupie.openfmi.net/blog/?feed=rss2";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            List<BlogPost> entries = new List<BlogPost>();
            foreach (SyndicationItem item in feed.Items)
            {
                entries.Add(new BlogPost()
                {
                    Title = item.Title.Text,
                    Summary = item.Summary.Text,
                    Date = item.PublishDate.Date.ToShortDateString(),
                    Link = item.Links.FirstOrDefault().Uri.ToString()
                });
            }
            return entries;
        }
    }
}
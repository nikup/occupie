using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Occupie.Models;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Occupie.RSSreader
{
    public class Reader
    {
        public List<BlogPost> RSS(int numberOfPosts)
        {
            string url = "http://occupie.openfmi.net/blog/?feed=rss2";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            List<BlogPost> entries = new List<BlogPost>();
            foreach (SyndicationItem item in feed.Items)
            {
                string content = "";
                foreach (SyndicationElementExtension ext in item.ElementExtensions)
                {
                    if (ext.GetObject<XElement>().Name.LocalName == "encoded")
                        content = ext.GetObject<XElement>().Value;
                }
                entries.Add(new BlogPost()
                {
                    Title = item.Title.Text,
                    Summary = getSummary(content),
                    Date = item.PublishDate.DateTime.ToString("dd.MM.yyyy"),
                    Link = item.Links.FirstOrDefault().Uri.ToString(),
                    //Author = item.Contributors[0].Name
                });
            }
            entries = entries.OrderByDescending(x => x.Date).Take(numberOfPosts).ToList();
            return entries;
        }

        private string getSummary(string content)
        {
            int indexOfMoreTag = content.IndexOf(@"<p><span id=""more-");
            if (indexOfMoreTag >= 0)
            {
                string result = content.Substring(0, indexOfMoreTag);
                return result;
            }

            return content;
        }
    }
}
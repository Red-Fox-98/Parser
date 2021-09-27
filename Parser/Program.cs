using HtmlAgilityPack;
using System;
using System.Collections;
using System.Drawing;
using System.Net;
using System.Text;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb wc = new HtmlWeb();
            wc.OverrideEncoding = Encoding.UTF8;
            HtmlDocument docHtml = wc.Load("https://forum.awd.ru/viewtopic.php?f=1011&t=165935");

            ArrayList list = new ArrayList();

            int value = 0;

            foreach (HtmlNode item in docHtml.DocumentNode.SelectNodes("//div[contains(@class, 'content')]//a/@href | //img/@data-src"))
            {
                Console.WriteLine(item.GetAttributeValue("data-src", null));
                list.Add(item.GetAttributeValue("data-src", null));
            }

            foreach (string itemImg in list)
            {
                if (itemImg != null)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(itemImg), @"Images\image" + value++ + ".jpg");
                    }
                }
                else { }
            }
        }
    }
}
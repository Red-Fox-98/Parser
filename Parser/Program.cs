using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //Enter link
            Console.Write("Enter link: ");
            string requestHtml = Console.ReadLine();
            Console.WriteLine();
            //Request HTML
            HtmlWeb wc = new HtmlWeb();
            wc.OverrideEncoding = Encoding.UTF8;
            HtmlDocument docHtml = wc.Load(requestHtml);
            //Array creation
            ArrayList list = new ArrayList();
            //Index image
            uint imageIndex = 0;
            uint imgIndex = 0;
            //Creating folder for image
            Directory.CreateDirectory("Images");
            //Search url image
            foreach (HtmlNode item in docHtml.DocumentNode.SelectNodes("//img/@data-src"))
            {
                Console.WriteLine("URL image № " + imgIndex);
                Console.Write(item.GetAttributeValue("data-src", null));
                Console.Write("\n");
                list.Add(item.GetAttributeValue("data-src", null));
                imgIndex++;
            }
            //Loading images
            Console.Write("\n");
            Console.WriteLine("Loading images...");
            Console.Write("\n");
            foreach (string itemImg in list)
            {
                if (itemImg != null)
                {
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile(new Uri(itemImg), @"Images\image" + imageIndex++ + ".jpg");
                            Console.WriteLine("Downloding image: " + itemImg);
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Failed to download image\n");
                            Console.Write(itemImg);
                        }
                    }
                }
                else { continue; }
            }
            Console.Write("\n");
            Console.Write("Loading is complete\n");
            Console.Write("\n");
            //Index image
            uint imageIndexInfo = 0;
            Console.WriteLine("Information about uploaded images\n");
            //Displaying image information
            for (int i = 0; i < imageIndex; i++)
            {
                Image img = Image.FromFile(@"Images\image" + imageIndexInfo + ".jpg");
                Console.WriteLine("Image №: " + imageIndexInfo);
                Console.WriteLine("Image dimensions : " + img.Size);

                double length = new FileInfo(@"Images\image" + imageIndexInfo + ".jpg").Length;
                Console.Write(length /= 1024);
                Console.Write(" Кб\n");
                imageIndexInfo++;
                img.Dispose();                
            }
            Console.WriteLine("Success, press enter");
            Console.ReadLine();
            //Deleting folder"
            Directory.Delete("Images", true);
        }
    }
}
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    public sealed class WS
    {
        private static volatile WS instance;
        private static readonly object locker = new object();

        private WS()
        {

        }

        public static WS Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new WS();
                    }
                }

                return instance;
            }
        }

        public void RunWebScrapper(int  numberOfImages)
        {
            Console.WriteLine("Provide the term you want to search on Google Images: ");
            string keyword = Console.ReadLine();
            var htmlImages = @"https://www.google.com/search?q=" + keyword + "&tbm=isch";
            HtmlWeb googleImages = new HtmlWeb();
            var htmlDoc = googleImages.Load(htmlImages);
            int i = 0;


            try
            {
                foreach (var link in htmlDoc.DocumentNode.Descendants("a"))
                {
                    var srcValue = link.GetAttributeValue("href", string.Empty);
                    if (srcValue.Contains("imgres?imgurl="))
                    {
                        using (var client = new WebClient())
                        {
                            string srcToDownload = srcValue.Replace(@"/imgres?imgurl=", "");
                            srcToDownload = srcToDownload.Substring(0, srcToDownload.IndexOf('&'));

                            client.DownloadFile(srcToDownload, @"C:\Users\seash\Desktop\downloaded\" + keyword + "_" + i.ToString() + ".jpg");
                            i++;
                        }
                    }

                    if (i > numberOfImages)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        


    }
}

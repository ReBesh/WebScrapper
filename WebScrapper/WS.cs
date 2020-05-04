using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace WebScrapper
{
    public sealed class WS
    {
        private static volatile WS sb;
        private static readonly object go = new object();



        public static WS SB
        {
            get
            {
                if (sb != null)
                    return sb;

                lock (go)
                {
                    if (sb == null)
                    {
                        sb = new WS();
                    }
                }

                return sb;
            }
        }


        public void ProblemSolution()

        {
            Console.WriteLine("Provide the number of images you want to download: ");
            int givennumber = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Provide the term you want to search on Google Images: ");
            string term = Console.ReadLine();
            var img = @"https://www.google.com/search?q=" + term + "&tbm=isch";
            HtmlWeb googleImages = new HtmlWeb();
            var ff = googleImages.Load(img);
            int i = 0;


            try
            {
                foreach (var link in ff.DocumentNode.Descendants("a"))
                {
                    var srcValue = link.GetAttributeValue("href", string.Empty);
                    if (srcValue.Contains("imgres?imgurl="))
                    {
                        using (var buy = new WebClient())
                        {
                            string srcToDownload = srcValue.Replace(@"/imgres?imgurl=", "");
                            srcToDownload = srcToDownload.Substring(0, srcToDownload.IndexOf('&'));

                            buy.DownloadFile(srcToDownload, @"C:\Users\seash\Desktop\downloaded\" + term + "_" + i.ToString() + ".jpg");
                            i++;
                        }
                    }

                    if (i > givennumber)
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

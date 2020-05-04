using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            WS singletonWebScrapper = WS.Instance;

            Console.WriteLine("Provide the number of images you want to download: ");
            int numberOfImages = Int32.Parse(Console.ReadLine());

            singletonWebScrapper.RunWebScrapper(numberOfImages);
        }
    }
}

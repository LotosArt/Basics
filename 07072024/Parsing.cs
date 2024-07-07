using System.Net;
using HtmlAgilityPack;

namespace LibLesson._07072024;

public class Parsing
{
    public static void Main(string[] args)
    {
        string code = "";
        using (WebClient client = new WebClient())
        {
            code = client.DownloadString("https://xorate.com/rejting-stiralnyh-mashin-2023-goda");
        }

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(code);

        HtmlNodeCollection titlesH1 = doc.DocumentNode.SelectNodes("//h1");
        Console.WriteLine(titlesH1.Count);
        HtmlNodeCollection titlesH4 = doc.DocumentNode.SelectNodes("//h4");
        Console.WriteLine(titlesH4.Count);
        HtmlNodeCollection tags = doc.DocumentNode.SelectNodes("//a[@class='bar']//span");
        foreach (HtmlNode tag in tags)
        {
            Console.WriteLine(tag.InnerText);
        }
        HtmlNodeCollection data = doc.DocumentNode.SelectNodes("//ul[@class='ul-crumbs']//li");
        Console.WriteLine($"Views - {data[1].InnerText.Trim()}");
        HtmlNode like = doc.DocumentNode.SelectSingleNode("//li[@title='Одобрения']");
        Console.WriteLine($"Likes - {like.InnerText}");
        Console.WriteLine($"Likes - {data[3].InnerText}");
    }
}
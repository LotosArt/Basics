namespace DomainApp;

class Program
{
    static void Main(string[] args)
    {
        string[] urls = {
            "https://news.liga.net/",
            "https://news.liga.net/somnews",
            "https://news.liga.net/somnews.html",
            "www.news.liga.net/somnews",
            "news.liga.net/somnews"
        };
        
        foreach (var url in urls)
        {
            try
            {
                string domainAndZone = DomainCutter.DomainCut.GetDomainAndZone(url);
                Console.WriteLine($"URL: {url} => Domain and Zone: {domainAndZone}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"URL: {url} => Error: {ex.Message}");
            }
        }

    }
}
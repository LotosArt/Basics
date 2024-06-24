using System.Text.RegularExpressions;

namespace DomainCutter;

public class DomainCut
{
    public static string GetDomainAndZone(string url)
    {
        // Регулярное выражение для извлечения домена и зоны
        string pattern = @"^(?:https?:\/\/)?(?:www\.)?([^\/]+)";
        Match match = Regex.Match(url, pattern);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            return "Invalid URL format";
        }
    }
}
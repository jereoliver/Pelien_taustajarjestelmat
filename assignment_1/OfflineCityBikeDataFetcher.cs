using System;
using System.Threading.Tasks;
using System.Linq;
using System.IO;




public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
{
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        if (stationName.Any(char.IsDigit))
        {
            throw new ArgumentException("Contains numbers");
        }

        StreamReader sr = new StreamReader("bikedata.txt");
        String line;

        while ((line = await sr.ReadLineAsync()) != null)
        {
            line = line.Trim();
            if (line.StartsWith(stationName))
            {
                int found = line.IndexOf(": ");
                int bikesNumber = Convert.ToInt32(line.Substring(found + 2));
                return bikesNumber;
            }
        }
        throw new NotFoundException("Station not found.");
    }

}
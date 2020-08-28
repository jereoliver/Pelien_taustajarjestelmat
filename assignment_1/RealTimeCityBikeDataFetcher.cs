using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;


public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
{
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        if (stationName.Any(char.IsDigit))
        {
            throw new ArgumentException("Contains numbers");
        }
    
        HttpClient client = new HttpClient();
        string osoite = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
        string stations =  await client.GetStringAsync(osoite);
        BikeRentalStationList list = JsonConvert.DeserializeObject<BikeRentalStationList>(stations);

        foreach(var station in list.stations)
        {
            if (stationName == station.name)
            {
                return station.bikesAvailable;
            }

        }
        throw new NotFoundException("Station not found.");
    }

}
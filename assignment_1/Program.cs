using System;
using System.Threading.Tasks;


namespace assignment_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ICityBikeDataFetcher dataFetcher = new RealTimeCityBikeDataFetcher();
            int bikes = 5000;
            String stationName = "Kaivopuisto";

            try
            {
                if (args[0] == "realtime")
                {
                    Console.WriteLine("Tervetuloa reaaliaikaiseen kaupunkifillaribottiin! Haetaan pyorien maaraa asemalta: " + stationName);
                    // ei muuteta dataFetcheria koska se on jo RealTimeCityB.... pakko alustaa if:n ulkopuolella että voi käyttää myöhemmin, muuten valittaa ettei ole alustettu mihinkään
                }
                else if (args[0] == "offline")
                {
                    dataFetcher = new OfflineCityBikeDataFetcher(); // muutetaan dataFetcher offline-toteutusta käyttäväksi
                    Console.WriteLine("Tervetuloa offline kaupunkifillaribottiin! Kerrotaan mika pyorien maara joskus oli asemalla: " + stationName);
                }
                else
                {
                    throw new Exception("Argument is not realtime/offline");
                }
                bikes = await dataFetcher.GetBikeCountInStation(stationName);
            }

            catch (ArgumentException e)
            {
                Console.WriteLine("Invalid Argument:" + e.Message);
            }
            catch (NotFoundException n)
            {
                Console.WriteLine("Not found: " + n.Message);
            }
            catch (Exception m)
            {
                Console.WriteLine("No arguments found: " + m.Message);
            }
            if (bikes != 5000)
                Console.WriteLine(stationName + ": " + bikes); // printataan pyörien määrä vain jos se on saatu noudettua API:sta
        }
    }
}

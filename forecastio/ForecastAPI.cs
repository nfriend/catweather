using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace forecastio
{
    public static class ForecastAPI
    {
        public static string GetForecast(string apiKey, float latitude, float longitude)
        {
            var client = new HttpClient();
            var t = client.GetStringAsync(string.Format("https://api.forecast.io/forecast/{0}/{1},{2}", apiKey, latitude, longitude));
            t.Wait();
            return t.Result;
        }
    }
}

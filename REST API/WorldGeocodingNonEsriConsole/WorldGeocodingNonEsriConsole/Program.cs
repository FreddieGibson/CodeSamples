using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Path = System.IO.Path;

namespace WorldGeocodingNonEsriConsole
{
    class Program
    {
        private const string TestSingleAddress = "380 New York St, Redlands, CA 92373";
        private static readonly string _desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string _results = Path.Combine(_desktop, "WorldGeocodingResults.txt");

        private const string WorldGeocodingUrl =
            "http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/find?{0}&outFields={1}&maxLocations={2}&f={3}";

        static void Main()
        {
            const string outFields = "Loc_name,Score,Match_addr,X,Y";
            const int maxLocations = 20;
            const string format = "json";
            string inFields = string.Format("text={0}", TestSingleAddress);

            Console.Write("Geocoding...");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string[] candidates = WorldGeocodeAddress(WorldGeocodingUrl, inFields, outFields, maxLocations, format);
            stopWatch.Stop(); TimeSpan timeSpan = stopWatch.Elapsed;

            Console.WriteLine("Done. [Time: {0:00}:{1}]\n", Math.Floor(timeSpan.TotalMinutes), timeSpan.ToString("ss\\.ff"));

            if (candidates.Length > 0)
            {
                Console.WriteLine("The following candidates were returned:");
                using (StreamWriter sw = new StreamWriter(_results))
                {
                    sw.WriteLine("World Geocoding Results: \"{0}\"", TestSingleAddress);

                    for (int i = 0; i < candidates.Length; i++)
                    {
                        sw.WriteLine("  {0:00}. {1}\n", i + 1, candidates[i]);
                    }
                }
            }
            else { Console.WriteLine("No candidates returned.\n"); }
            
            Console.Write("Press Enter to exit...");
            Console.ReadLine();
        }

        static string[] WorldGeocodeAddress(string url, string inFields, string outFields, int maxLocations, string format)
        {
            inFields = inFields.Replace("&", "%26");
            inFields = HttpUtility.ParseQueryString(inFields).ToString();

            outFields = HttpUtility.UrlEncode(outFields);
            format = HttpUtility.UrlEncode(format);

            return MakeRequest(string.Format(url, inFields, outFields, maxLocations, format));
        }

        static string[] MakeRequest(string requestUrl)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

            request.Method = WebRequestMethods.Http.Get;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));

                string jsonResponse;
                using (var sr = new System.IO.StreamReader(response.GetResponseStream())) { jsonResponse = sr.ReadToEnd(); }
                return Regex.Split(jsonResponse, "},{");
            }
        }
    }
}

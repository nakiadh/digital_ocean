namespace services.CheckWebsite;

using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebsiteInfoService;


public class CheckWebsite 
{
  static readonly string BaseUrl = @"https://jsonplaceholder.typicode.com/todos";

  static CheckWebsite(){}

  public static async Task<string> GetLastestAsync(string id){
    var output = await CheckWebsite.GetLastestAsync(id);

        return output!;
    }

    public static async Task<string> GetLastestObservationForStationAsync(string ICAOStationId)
    {
        string responseBody = "";
        try{

            // if (!WikipediaAirportScraper.ValidateTexasAirportICAO(ICAOStationId))
            // {
            //     throw new HttpRequestException("ICAO id is invalid");
            // }
            using (var client = new HttpClient())
            {

                // make sure we accept JSON+LD
                client.DefaultRequestHeaders.Add("Accept","application/ld+json");
                client.DefaultRequestHeaders.Add("User-Agent","MyTestRuntime");

                string url = $"{BaseUrl}/stations/{ICAOStationId}/observations/latest";

                responseBody = await client.GetStringAsync(url);
                //good resource for success codes: https://www.airport-data.com/api/doc.php#status                

            }

        }catch (HttpRequestException exp)
        {
            Console.WriteLine(exp.Message);
        }

        return responseBody;        

    }

    public static Website? DeserializeWeatherStationObservationFromJSON(string data)
    {

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true
        };        

        Website? obs = JsonSerializer.Deserialize<Website>(data, options);
        return obs;

    }

    public static Website ParseObservationFromString(string data)
    {
        // Create a JsonNode DOM from a JSON string.
        // note: the "bang" here is the null-forgiving operator: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving
        JsonNode obsNode = JsonNode.Parse(data)!;

        //get geometry
        JsonNode geometryNode = obsNode!["geometry"]!;
        string geometry = geometryNode.ToString();
        Console.WriteLine($"Confirming node: {geometry}");

        //get elevation
        JsonNode elevationNode = obsNode!["elevation"]!;
        Dictionary<string, string> elevation = elevationNode.GetValue<Dictionary<string, string>>();
        Console.WriteLine($"Confirming node: {elevation.Keys}");

        Website obs = new Website();

        return obs;
    }

}
  
    


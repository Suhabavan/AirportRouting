using Serilog;

namespace AirportRoutingAPI.Services
{
    public class AirportService : IAirportService
    {

        public List<string> GetDestinations(string originAirportCode)
        {
            try
            {
                Log.Information($"Request for origin airport: {originAirportCode} - Retrieving destinations...");
                List<string> destinations = GetDestinationAirports(originAirportCode);

                if (destinations.Count == 0)
                {
                    Log.Warning($"No destinations found for origin airport: {originAirportCode}");
                    throw new ArgumentException($"No destinations found for origin airport: {originAirportCode}");
                }

                Log.Information($"Request for origin airport: {originAirportCode} - Retrieved destinations: {string.Join(", ", destinations)}");
                return destinations;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in AirportService: {ex.Message}");
                throw;
            }
        }

        private List<string> GetDestinationAirports(string originAirportCode)
        {
            var destinations = new Dictionary<string, List<string>>
            {
                { "NYC", new List<string> { "LON", "PAR", "LAX", "ROM" } },
                { "LAX", new List<string> { "NYC", "LON", "MIA" } }
            };

            if (destinations.ContainsKey(originAirportCode))
            {
                return destinations[originAirportCode];
            }

            return new List<string>();
        }
    }
}



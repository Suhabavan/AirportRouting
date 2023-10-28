namespace AirportRoutingAPI.Services
{
    public interface IAirportService
    {
        List<string> GetDestinations(string originAirportCode);
    }
}

namespace WebAPIService.Models
{
    public class GRPCSettings : IGRPCSettings
    {
        public string Address { get; set; }
    }

    public interface IGRPCSettings
    {
        string Address { get; set; }
    }
}
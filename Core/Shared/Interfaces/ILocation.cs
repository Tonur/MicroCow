namespace Shared.Interfaces
{
    public interface ILocation
    {
        string EarTag { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}

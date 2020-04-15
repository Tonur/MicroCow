namespace Shared.Interfaces
{
    public interface ICowLocation
    {
        string EarTag { get; set; }
        string Name { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}

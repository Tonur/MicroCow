namespace Shared.Interfaces
{
    public interface IApiResponse<T>
    {
        ApiResponseCode Code { get; set; }

        T Value { get; set; }
    }
}

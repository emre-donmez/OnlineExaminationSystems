
namespace OnlineExaminationSystems.UI.Helpers
{
    public interface IApiRequestHelper
    {
        Task<bool> DeleteAsync(string endpoint);
        Task<T> GetAsync<T>(string endpoint);
        Task<T> GetAsync<T>(string endpoint, object data);
        Task<T> PostAsync<T>(string endpoint, object data);
        Task<T> PutAsync<T>(string endpoint, object data);
    }
}

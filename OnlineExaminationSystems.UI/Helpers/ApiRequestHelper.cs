using System.Text;
using System.Text.Json;

namespace OnlineExaminationSystems.UI.Helpers;

public class ApiRequestHelper : IApiRequestHelper
{
    private HttpClient _client;

    public ApiRequestHelper(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<T> GetAsync<T>(string endpoint, object data)
    {
        string queryString = BuildQueryString(data);

        HttpResponseMessage response = await _client.GetAsync(endpoint + queryString);

        return await HandleResponse<T>(response);
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        HttpResponseMessage response = await _client.GetAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var stringContent = CreateStringContent(data);

        HttpResponseMessage response = await _client.PostAsync(endpoint, stringContent);

        return await HandleResponse<T>(response);
    }

    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        var stringContent = CreateStringContent(data);

        HttpResponseMessage response = await _client.PutAsync(endpoint, stringContent);

        return await HandleResponse<T>(response);
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        HttpResponseMessage response = await _client.DeleteAsync(endpoint);

        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode ? true : false;
    }

    private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        string responseData = await response.Content.ReadAsStringAsync();

        if (typeof(T) == typeof(string))
            return (T)(object)responseData;

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(responseData, jsonSerializerOptions);
    }

    private static StringContent CreateStringContent(object data)
    {
        var bodyJson = JsonSerializer.Serialize(data);
        return new StringContent(bodyJson, Encoding.UTF8, "application/json");
    }

    private string BuildQueryString(object queryParams)
    {
        if (queryParams == null)
            return "";

        var properties = queryParams.GetType().GetProperties()
                   .Where(p => p.GetValue(queryParams, null) != null)
                   .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(queryParams, null).ToString())}");

        return "?" + string.Join("&", properties);
    }
}
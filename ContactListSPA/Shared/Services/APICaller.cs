using System.Net.Http.Headers;

namespace ContactListSPA.Shared.Services;
/// <summary>
/// Class reponsible for API calls.
/// </summary>
public class APICaller : IAPICaller
{
    private readonly HttpClient _httpClient;

    public APICaller(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    // URI of the API.
    public string BaseURI { get; set; } = "https://localhost:5001/api/";

    /// <summary>
    /// Sends request to the API.
    /// </summary>
    /// <param name="httpMethod">Request method type.</param>
    /// <param name="endpoint">API endpoint.</param>
    /// <param name="authHeadValue">Value holding JWT.</param>
    /// <param name="content">Body of the request.</param>
    /// <returns>Response from the API.</returns>
    public async Task<HttpResponseMessage> SendRequest(string httpMethod, string endpoint, AuthenticationHeaderValue? authHeadValue = null, HttpContent? content = null)
    {
        var requestMessage = new HttpRequestMessage()
        {
            Method = new HttpMethod(httpMethod),
            RequestUri = new Uri(BaseURI+endpoint),
            Content = content
        };

        requestMessage.Headers.Authorization = authHeadValue;

        return await _httpClient.SendAsync(requestMessage);
    }
}

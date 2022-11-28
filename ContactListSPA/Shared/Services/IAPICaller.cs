using System.Net.Http.Headers;

namespace ContactListSPA.Shared.Services;
/// <summary>
/// Interface reponsible for API calls.
/// </summary>
public interface IAPICaller
{
    public string BaseURI { get; set; }
    Task<HttpResponseMessage> SendRequest(
        string httpMethod, 
        string endpoint, 
        AuthenticationHeaderValue? authHeadValue = null, 
        HttpContent? content = null);
}

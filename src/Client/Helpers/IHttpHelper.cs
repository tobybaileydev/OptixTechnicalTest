namespace OptixTechnicalTest.Client.Helpers;

public interface IHttpHelper
{
    Task NavigateToErrorPageAsync();
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
}

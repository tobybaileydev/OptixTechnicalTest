namespace OptixTechnicalTest.Client.Helpers;

public class HttpHelper(
    HttpClient httpClient,
    NavigationManager navigationManager
    ) : IHttpHelper
{
    public async Task NavigateToErrorPageAsync()
    {
        navigationManager.NavigateTo("/ErrorPage");
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
    {
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage,
                HttpCompletionOption.ResponseHeadersRead, CancellationToken.None);

        return httpResponseMessage;
    }
}

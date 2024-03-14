using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace OptixTechnicalTest.Client.Services;

public class ApiRequestService(
    IHttpHelper httpHelper
    ) : IApiRequestService
{
    public async Task<int> GetMaxRecordsAsync()
    {
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.Method = HttpMethod.Get;
        httpRequestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
        httpRequestMessage.RequestUri = new Uri("api/v1/Movies/GetMaxRecordsAsync", UriKind.RelativeOrAbsolute);

        var httpResponseMessage = await httpHelper.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseString))
            {
                await httpHelper.NavigateToErrorPageAsync();
                return 0;
            }

            if (!int.TryParse(responseString, out var maxRecords))
            {
                await httpHelper.NavigateToErrorPageAsync();
                return 0;
            }

            return maxRecords;
        }
        else
        {
            await httpHelper.NavigateToErrorPageAsync();
            return 0;
        }
    }

    public async Task<List<MovieDataModel>> GetMovieDataModelsAsync(string searchTitle, int searchRecords)
    {
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.Method = HttpMethod.Get;
        httpRequestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
        httpRequestMessage.RequestUri = new Uri($"api/v1/Movies/GetByTitleAsync?title={searchTitle}&resultsCount={searchRecords}", UriKind.RelativeOrAbsolute);

        var httpResponseMessage = await httpHelper.SendAsync(httpRequestMessage);

        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(responseString))
        {
            await httpHelper.NavigateToErrorPageAsync();
            return [];
        }

        if (httpResponseMessage.IsSuccessStatusCode)
        {

            var movies = JsonConvert.DeserializeObject<List<MovieDataModel>>(responseString);

            if (movies == null)
            {
                await httpHelper.NavigateToErrorPageAsync();
                return [];
            }

            return movies;
        }
        else
        {
            Console.WriteLine(responseString);
            await httpHelper.NavigateToErrorPageAsync();
            return [];
        }
    }
}

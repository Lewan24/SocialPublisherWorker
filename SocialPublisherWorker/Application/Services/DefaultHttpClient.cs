using System.Net.Http.Json;
using System.Text.Json;
using SocialPublisherWorker.Application.Interfaces;

namespace SocialPublisherWorker.Application.Services;

public sealed class DefaultHttpClient(
    HttpClient httpClient,
    ILogger<DefaultHttpClient> logger)
    : IDefaultHttpClient
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    public async Task<TResponse> FetchAsync<TResponse>(
        string url,
        CancellationToken ct = default)
    {
        using var response = await httpClient.GetAsync(
            url,
            HttpCompletionOption.ResponseHeadersRead,
            ct);

        return await HandleResponse<TResponse>(
            response,
            ct);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string url,
        TRequest request,
        CancellationToken ct = default)
    {
        using var response = await httpClient.PostAsJsonAsync(
            url,
            request,
            JsonOptions,
            ct);

        return await HandleResponse<TResponse>(
            response,
            ct);
    }

    public async Task<string> PostFormAsync(
        string url,
        IDictionary<string, string> form,
        CancellationToken ct = default)
    {
        using var content = new FormUrlEncodedContent(form);

        using var response = await httpClient.PostAsync(
            url,
            content,
            ct);
        
        return await response.Content.ReadAsStringAsync(ct);
    }
    
    private async Task<T> HandleResponse<T>(
        HttpResponseMessage response,
        CancellationToken ct)
    {
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content
                .ReadAsStringAsync(ct);

            logger.LogError(
                "HTTP request failed. StatusCode: {StatusCode}, Content: {Content}",
                response.StatusCode,
                content);

            throw new HttpRequestException(
                $"Request failed with status code {(int)response.StatusCode}");
        }

        var result = await response.Content
                .ReadFromJsonAsync<T>(JsonOptions, ct);

        if (result is null)
        {
            throw new InvalidOperationException(
                "Response deserialization returned null");
        }

        return result;
    }
}
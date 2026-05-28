namespace SocialPublisherWorker.Application.Interfaces;

public interface IDefaultHttpClient
{
    Task<TResponse> FetchAsync<TResponse>(
        string url,
        CancellationToken ct = default);

    Task<TResponse> PostAsync<TRequest, TResponse>(
        string url,
        TRequest request,
        CancellationToken ct = default);
    
    Task<string> PostFormAsync(
        string url,
        IDictionary<string, string> form,
        CancellationToken ct = default);
}
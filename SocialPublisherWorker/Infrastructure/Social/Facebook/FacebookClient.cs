using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookClient
{
    //TODO: Implement facebook client
    public async Task<string> PublishPostAsync(
        HttpClient httpClient,
        FacebookOptions options,
        CalendarPost post,
        CancellationToken ct)
    {
        var facebookAuthApi = new Uri($"https://graph.facebook.com/oauth/access_token");
        var facebookGraphApi = new Uri("https://graph.facebook.com/v25.0");

        //var authGetAccessTokenUri = new Uri($"{facebookAuthApi}?client_id={options.AppId}&client_secret={options.AppSecret}&grant_type=client_credentials");
        //var authResponse = await httpClient.GetStringAsync(authGetAccessTokenUri, ct);
        
        //var authResult = JsonSerializer.Deserialize<FacebookAuthApiResponse>(authResponse);
        
        //var getPageDetailsResult = await httpClient.GetStringAsync($"{facebookGraphApi}/{options.PageId}?access_token={authResult?.access_token}", ct);
        
        //return getPageDetailsResult;
        
        var httpContent = new StringContent(JsonSerializer.Serialize(new FacebookCreatePostRequest("Hello world", options.AccessToken)));
        var result = await httpClient.PostAsync($"{facebookGraphApi}/{options.PageId}/feed", httpContent, ct);

        return await result.Content.ReadAsStringAsync(ct);
    }
}

public record FacebookAuthApiResponse(string access_token, string token_type); 
public record FacebookCreatePostRequest(string message, string access_token);
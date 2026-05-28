using Microsoft.Extensions.Options;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookClient(
    IDefaultHttpClient httpClient,
    IOptions<FacebookOptions> options,
    ILogger<FacebookClient> logger)
{
    private readonly Uri _facebookGraphApi = new("https://graph.facebook.com/v25.0");
    private readonly FacebookOptions _options = options.Value;
    
    public async Task<string> TestTokenAndConnection(CancellationToken ct)
    {
        try
        {
            var pageUri = new Uri($"{_facebookGraphApi}/{_options.PageId}");
            logger.LogInformation("Requesting: {PageUri}", pageUri);
            
            var result = await httpClient.FetchAsync<string>(
                $"{pageUri}?access_token={_options.AccessToken}", ct);

            return result;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    public async Task<string> PublishPostAsync(
        CalendarPost post,
        CancellationToken ct)
    {
        try
        {
            var requestValues = new Dictionary<string, string>
            {
                ["caption"] = post.Caption,
                ["url"] = post.PhotoUrl
            };

            var url =
                $"{_facebookGraphApi}/{_options.PageId}/photos?access_token={_options.AccessToken}";
            logger.LogInformation("Requesting: {PageUri}", url);
            
            return await httpClient.PostFormAsync(url, requestValues, ct);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    public async Task<bool> PublishCommentsAsync(
        string postId,
        CancellationToken ct)
    {
        var comments = new[]
        {
            "Poniedziałek / Monday",
            "Wtorek / Tuesday",
            "Środa / Wednesday",
            "Czwartek / Thursday",
            "Piątek / Friday",
            "Sobota / Saturday",
            "Niedziela / Sunday"
        };

        try
        {
            foreach (var comment in comments)
            {
                Console.WriteLine($"Processing ('{comment}')...");
                await PublishCommentAsync(
                    postId,
                    comment,
                    ct);
            }
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private async Task PublishCommentAsync(
        string postId,
        string comment,
        CancellationToken ct)
    {
        var requestValues = new Dictionary<string, string>
        {
            ["message"] = comment
        };
        
        using var httpContent = new FormUrlEncodedContent(requestValues);
        
        var url = $"{_facebookGraphApi}/{postId}/comments";
        logger.LogInformation("Requesting: {PageUri}", url);
        
        await httpClient.PostFormAsync(
            $"{url}?access_token={_options.AccessToken}", 
            requestValues, ct);
    }
}
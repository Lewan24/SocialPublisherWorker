using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookClient
{
    private readonly Uri _facebookGraphApi = new("https://graph.facebook.com/v25.0");
   
    //TODO: Change primitive returns to custom object with status, message etc.
    public async Task<string> PublishPostAsync(
        HttpClient httpClient,
        FacebookOptions options,
        CalendarPost post,
        CancellationToken ct)
    {
        // TODO: Implement retrying
        try
        {
            var requestValues = new Dictionary<string, string>
            {
                ["caption"] = post.Caption,
                ["url"] = post.PhotoUrl
            };
            
            using var httpContent = new FormUrlEncodedContent(requestValues);
            var result = await httpClient.PostAsync(
                requestUri: $"{_facebookGraphApi}/{options.PageId}/photos?access_token={options.AccessToken}", 
                content: httpContent,
                cancellationToken: ct);
            
            return await result.Content.ReadAsStringAsync(ct);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    public async Task<bool> PublishCommentsAsync(
        HttpClient httpClient,
        FacebookOptions options,
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
                    httpClient,
                    options,
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
        HttpClient httpClient,
        FacebookOptions options,
        string postId,
        string comment,
        CancellationToken ct)
    {
        //TODO: implement retrying
        var requestValues = new Dictionary<string, string>
        {
            ["message"] = comment
        };
        
        using var httpContent = new FormUrlEncodedContent(requestValues);
        var response = await httpClient.PostAsync(
            requestUri: $"{_facebookGraphApi}/{postId}/comments?access_token={options.AccessToken}", 
            content: httpContent,
            cancellationToken: ct);
        
        response.EnsureSuccessStatusCode();
    }
}
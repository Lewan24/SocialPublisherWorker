using System.Text.Json;
using Microsoft.Extensions.Options;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public class FacebookPublisher(ILogger<FacebookPublisher> logger, HttpClient http, IOptions<FacebookOptions> fbOptions) : ISocialPublisher
{
    private readonly FacebookClient _facebookClient = new();
    
    public SocialPlatform Platform => SocialPlatform.Facebook;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="post"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created post ID</returns>
    public async Task PublishAsync(CalendarPost post, CancellationToken cancellationToken)
    {
        logger.LogInformation("Publishing post {@postTitle} to facebook page...", post.Caption);
        var createPostResult = await _facebookClient.PublishPostAsync(http, fbOptions.Value, post, cancellationToken);
        logger.LogInformation("Post published");
        logger.LogInformation("Created post Id: {@Post}", createPostResult);

        var createdPost = JsonSerializer.Deserialize<FacebookCreatedPostResponse>(createPostResult);
        
        logger.LogInformation("Attempting to create comments to created post...");
        var postCommentsResult = await _facebookClient.PublishCommentsAsync(http, fbOptions.Value, createdPost?.post_id!, cancellationToken);
        logger.LogInformation("Comments publishing status: {@Status}", postCommentsResult ?  "Success" : "Failure");
    }
    
    /// <summary>
    /// Helper for Facebook http response
    /// </summary>
    /// <param name="id"></param>
    /// <param name="post_id"></param>
    private record FacebookCreatedPostResponse(string id, string post_id);
}
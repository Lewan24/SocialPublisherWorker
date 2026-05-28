using System.Text.Json;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public class FacebookPublisher(
    ILogger<FacebookPublisher> logger,
    FacebookClient facebookClient) : ISocialPublisher
{
    public SocialPlatform Platform => SocialPlatform.Facebook;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="post"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created post ID</returns>
    public async Task PublishAsync(CalendarPost post, CancellationToken cancellationToken)
    {
        // Test if env works for simple request
        // var result = await facebookClient.TestTokenAndConnection(cancellationToken);
        // logger.LogInformation(result);
        // return;
        
        logger.LogInformation("Publishing post {@postTitle} to facebook page...", post.Caption);
        var createPostResult = await facebookClient.PublishPostAsync(post, cancellationToken);
        logger.LogInformation("Post published");
        logger.LogInformation("Created post Id: {@Post}", createPostResult);

        var createdPost = JsonSerializer.Deserialize<FacebookCreatedPostResponse>(createPostResult);
        
        logger.LogInformation("Attempting to create comments to created post...");
        var postCommentsResult = await facebookClient.PublishCommentsAsync(createdPost?.post_id!, cancellationToken);
        logger.LogInformation("Comments publishing status: {@Status}", postCommentsResult ?  "Success" : "Failure");
    }
    
    /// <summary>
    /// Helper for Facebook http response
    /// </summary>
    /// <param name="id"></param>
    /// <param name="post_id"></param>
    private record FacebookCreatedPostResponse(string id, string post_id);
}
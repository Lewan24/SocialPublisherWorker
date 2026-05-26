using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public class FacebookPublisher(ILogger<FacebookPublisher> logger) : ISocialPublisher
{
    private readonly FacebookClient _facebookClient = new();
    
    public SocialPlatform Platform => SocialPlatform.Facebook;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="post"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created post ID</returns>
    public async Task<string> PublishAsync(CalendarPost post, CancellationToken cancellationToken)
    {
        logger.LogInformation("Publishing post {@postTitle} to facebook page", post.Title);
        var createPostId = await _facebookClient.PublishPostAsync("", "", post, cancellationToken);
        
        logger.LogInformation("Post published");
        return createPostId;
    }
}
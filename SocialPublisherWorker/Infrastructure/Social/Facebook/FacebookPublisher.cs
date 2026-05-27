using Microsoft.Extensions.Options;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Application.Services;
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
    public async Task<string> PublishAsync(CalendarPost post, CancellationToken cancellationToken)
    {
        logger.LogInformation("Publishing post {@postTitle} to facebook page", post.Caption);
        var createPostId = await _facebookClient.PublishPostAsync(http, fbOptions.Value, post, cancellationToken);
        logger.LogInformation("Http result: {@Post}", createPostId);
        logger.LogInformation("Post published");
        return createPostId;
    }
}
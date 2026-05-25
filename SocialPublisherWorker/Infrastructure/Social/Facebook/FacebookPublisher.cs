using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

internal class FacebookPublisher : ISocialPublisher
{
    public SocialPlatform Platform => SocialPlatform.Facebook;

    public Task<string> PublishAsync(CalendarPost post, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
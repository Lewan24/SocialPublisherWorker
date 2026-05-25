using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Infrastructure.Social.Facebook;

public sealed class FacebookClient
{
    public async void PublishPostAsync(
        string pageId,
        string accessToken,
        CalendarPost post,
        CancellationToken ct)
    {
    }
}
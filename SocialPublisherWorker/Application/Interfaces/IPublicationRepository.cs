using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Application.Interfaces;

public interface IPublicationRepository
{
    Task<bool> DoesPostPublicationExistAsync(SocialPlatform platform, DateOnly weekStart);
    Task AddPostPublicationAsync(SocialPlatform platform, string externalPostId);
}
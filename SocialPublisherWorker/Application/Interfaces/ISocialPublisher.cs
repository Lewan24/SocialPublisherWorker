using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Application.Interfaces;

internal interface ISocialPublisher
{
    SocialPlatform Platform { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="post"><see cref="CalendarPost"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created post ID from social</returns>
    Task<string> PublishAsync(CalendarPost post, CancellationToken cancellationToken);
}
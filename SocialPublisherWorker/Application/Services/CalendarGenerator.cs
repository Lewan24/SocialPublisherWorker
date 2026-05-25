using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Application.Services;

public sealed class CalendarGenerator
    : ICalendarGenerator
{
    public CalendarPost GenerateNextWeek()
    {
        throw new NotImplementedException();
    }
}
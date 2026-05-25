using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Application.Interfaces;

public interface ICalendarGenerator
{
    CalendarPost GenerateNextWeek();
}
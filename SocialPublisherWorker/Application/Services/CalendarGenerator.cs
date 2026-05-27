using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Application.Services;

public sealed class CalendarGenerator(IClock clock)
    : ICalendarGenerator
{
    public CalendarPost GenerateNextWeek()
    {
        var startWeek = DateOnly.FromDateTime(clock.UtcNow.AddDays(1).DateTime);
        
        var newPost = new CalendarPost
        {
            PhotoUrl = "https://scontent.fgdn1-1.fna.fbcdn.net/v/t39.30808-6/705729373_3276352705906687_343019618255444986_n.jpg?_nc_cat=100&ccb=1-7&_nc_sid=aa7b47&_nc_ohc=J9KWpHmnt1AQ7kNvwEy7Jz8&_nc_oc=AdoST4P802deyZ-hTlzR_iFbXOEJJ-oEDFnRq8gL3PNRU5u6AbcaG-Lf9W4Mgz0UjHo&_nc_zt=23&_nc_ht=scontent.fgdn1-1.fna&_nc_gid=g89Zz_NmVDam4tUqvil8rQ&_nc_ss=7b2a8&oh=00_Af5Gcv9qbhCFZyXxA6e5S3XFJfpSYlJ8m1d1ErjKpJTMXQ&oe=6A1CECC7",
            Caption = $"Schedule: {startWeek:dd.MM} - {startWeek.AddDays(6):dd.MM}"
        };
        
        return newPost;
    }
}
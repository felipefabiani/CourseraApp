using EventEase.Models;
using System.Collections.Concurrent;

namespace EventEase.Services;

public class EventInMemoryService
{
    private ConcurrentBag<EventModel> _events = [
        new EventModel {
            Id = 1,
            Name = "Dublin Tech Pulse",
            Location = "Dublin",
            Date = new DateTime(2025, 9, 14, 10, 0, 0),
            Description = "Explore Dublin’s startup scene with talks, demos, and networking in the heart of the city."
        },
        new EventModel {
            Id = 2,
            Name = "Temple Bar TradFest",
            Location = "Dublin",
            Date = new DateTime(2025, 10, 5, 18, 30, 0),
            Description = "Live Irish music fills Temple Bar as local legends and newcomers share the stage together."
        },
        new EventModel {
            Id = 3,
            Name = "Phoenix Park Picnic",
            Location = "Dublin",
            Date = new DateTime(2025, 8, 24, 13, 0, 0),
            Description = "Bring a blanket and enjoy food trucks, games, and sunshine in Dublin’s iconic Phoenix Park."
        },
        new EventModel {
            Id = 4,
            Name = "Dublin Noir Book Fair",
            Location = "Dublin",
            Date = new DateTime(2025, 11, 2, 11, 0, 0),
            Description = "A celebration of crime fiction with author panels, signings, and mystery-themed activities."
        }
    ];

    public List<EventModel> GetAllEventModels()
    {
        return [.. _events.OrderBy(f => f.Id)];
    }

    public List<EventModel> SubmitEventModel(EventModel feedback)
    {
        ArgumentNullException.ThrowIfNull(feedback, "EventModel cannot be null.");

        if (feedback.Id == 0)
        {
            feedback.Id = _events.Count > 0 ? _events.Max(f => f.Id) + 1 : 1;
        }
        else
        {
            RemoveEventModel(feedback);
        }
        _events.Add(feedback);
        return _events.ToList();
    }

    public EventModel? GetEventModelById(int id)
    {
        return _events.FirstOrDefault(f => f.Id == id);
    }

    public void ClearEventModel()
    {
        _events.Clear();
    }
    public List<EventModel> RemoveEventModel(EventModel feedback)
    {
        _events = [.. _events.Where(f => f.Id != feedback.Id)];
        return _events.ToList();
    }
}

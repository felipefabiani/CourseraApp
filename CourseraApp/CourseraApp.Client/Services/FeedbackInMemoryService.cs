using CourseraApp.Client.Models;
using System.Collections.Concurrent;

namespace CourseraApp.Client.Services;

public class FeedbackInMemoryService
{
    private ConcurrentBag<Feedback> _feedbacks = [
        new Feedback { Id = 1, Name= "Felipe 1", Email = "f1@test.com", Comment = "Great course!" },
        new Feedback { Id = 2, Name= "Felipe 2", Email = "f2@test.com", Comment = "Very informative." },
        new Feedback { Id = 3, Name= "Felipe 3", Email = "f2@test.com", Comment = "Very informative." },
        new Feedback { Id = 4, Name= "Felipe 4", Email = "f2@test.com", Comment = "Very informative." }
    ];

    public List<Feedback> GetAllFeedbacks()
    {
        return [.. _feedbacks.OrderBy(f => f.Id)];
    }

    public List<Feedback> SubmitFeedback(Feedback feedback)
    {
        //Thread.Sleep(5000);
        ArgumentNullException.ThrowIfNull(feedback, "Feedback cannot be null.");

        if (feedback.Id == 0)
        {
            feedback.Id = _feedbacks.Count > 0 ? _feedbacks.Max(f => f.Id) + 1 : 1;
        }
        else
        {
            RemoveFeedback(feedback);
        }
        _feedbacks.Add(feedback);
        return _feedbacks.ToList();
    }

    public Feedback GetFeedbackById(int id)
    {
        return _feedbacks.FirstOrDefault(f => f.Id == id) ?? new();
    }

    public void ClearFeedback()
    {
        _feedbacks.Clear();
    }
    public List<Feedback> RemoveFeedback(Feedback feedback)
    {
        _feedbacks = [.. _feedbacks.Where(f => f.Id != feedback.Id)];
        return _feedbacks.ToList();
    }
}

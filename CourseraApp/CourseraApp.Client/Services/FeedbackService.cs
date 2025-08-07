using CourseraApp.Client.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace CourseraApp.Client.Services;

public class FeedbackService
{
    private readonly IJSRuntime _jsRuntime;
    private const string feedbackKey = "feedback";
    public FeedbackService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SaveFeedbackAsync(List<Feedback> feedbackList)
    {   
        ArgumentNullException.ThrowIfNull(feedbackList, "Feedback cannot be null.");
     
        // await Task.Delay(1000);
        

        var json = JsonSerializer.Serialize(feedbackList);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", feedbackKey, json);
    }
    public async Task<List<Feedback>> LoadFeedbackAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", feedbackKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<Feedback>>(json) ?? [];
    }

    public async Task SubmitFeedbackAsync(Feedback feedback)
    {
        ArgumentNullException.ThrowIfNull(feedback, "Feedback cannot be null.");
        
        var feedbackList = await LoadFeedbackAsync();
        if (feedback.Id == 0)
        {
            feedback.Id = feedbackList.Count > 0 ? feedbackList.Max(f => f.Id) + 1 : 1;
        }
        else
        {
            var existingFeedback = feedbackList.FirstOrDefault(f => f.Id == feedback.Id);
            if (existingFeedback != null)
            {
                feedbackList.Remove(existingFeedback);
            }
        }
        feedbackList.Add(feedback);
        await SaveFeedbackAsync([.. feedbackList.OrderBy(x => x.Id)]);
    }

    public async Task<Feedback> GetFeedbackByIdAsync(int id)
    {
        var feedbackList = await LoadFeedbackAsync();
        return feedbackList.FirstOrDefault(f => f.Id == id) ?? new ();
    }

    public async Task ClearFeedbackAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", feedbackKey);
    }
}

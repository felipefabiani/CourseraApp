using FluentValidation;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace EventEaseApp.Models;

public class UserSessionState
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; }  = string.Empty;
    public DateTime LastActiviteAt { get; private set; } = DateTime.UtcNow;
    public DateTime PreviousActiviteAt { get; private set; } = DateTime.UtcNow;
    public string CurrentPage { get; private set; } = String.Empty;
    public string PreviousPage { get; private set; } = String.Empty;
    public bool IsAuthenticated { get; private set; } = false;
    public List<EventModel> AttendedEvents { get; set; } = new();


    public event Action? OnChange;
    public void LogIn(string name, string email, NavigationManager nav)
    {
        Name = name;
        Email = email;
        PreviousActiviteAt = LastActiviteAt = DateTime.UtcNow;
        PreviousPage = CurrentPage = GetCurrentPage(nav);
        IsAuthenticated = true;
        AttendedEvents.Clear();
        NotifyStateChanged();
    }

    public void UpdatePage(NavigationManager nav)
    {
        PreviousPage = CurrentPage;
        CurrentPage = GetCurrentPage(nav);
        PreviousActiviteAt = LastActiviteAt;
        LastActiviteAt = DateTime.UtcNow;

        NotifyStateChanged();
    }
    public void LogOff()
    {
        Name = string.Empty;
        Email = string.Empty;
        PreviousActiviteAt = LastActiviteAt = DateTime.MinValue;
        PreviousPage = CurrentPage = string.Empty;
        IsAuthenticated = false;
        AttendedEvents.Clear();
        NotifyStateChanged();
    }
    public void NotifyStateChanged()
    {
        if (IsAuthenticated)
        {
            OnChange?.Invoke();
        }
    }

    private string GetCurrentPage(NavigationManager nav)
    {
        var currentPage = nav.Uri
            .Replace(nav.BaseUri, "")
            .Replace("/", " ");
        if (string.IsNullOrWhiteSpace(currentPage))
        {
            currentPage = "Home";
        }
        return currentPage;
    }
}

public class UserLogIn
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}
public class UserValidator : AbstractValidator<UserLogIn>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must be under 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be valid");

    }
}

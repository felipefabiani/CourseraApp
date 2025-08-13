using FluentValidation;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace EventEaseApp.Models;

public class UserSessionState
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }  = string.Empty;
    public DateTime LastActiviteAt { get; set; } = DateTime.UtcNow;
    public DateTime PreviousActiviteAt { get; set; } = DateTime.UtcNow;
    public string CurrentPage { get; set; } = String.Empty;
    public string PreviousPage { get; set; } = String.Empty;
    public bool IsAuthenticated { get; set; } = false;
    public List<EventModel> AttendedEvents { get; set; } = new();

    public UserSessionState LogIn(string name, string email, NavigationManager nav)
    {
        Name = name;
        Email = email;
        PreviousActiviteAt = DateTime.UtcNow;
        LastActiviteAt = DateTime.UtcNow;
        PreviousPage = CurrentPage = GetCurrentPage(nav);
        IsAuthenticated = true;
        AttendedEvents.Clear();
        return this;
    }

    public UserSessionState UpdatePage(NavigationManager nav)
    {
        PreviousPage = CurrentPage;
        CurrentPage = GetCurrentPage(nav);
        PreviousActiviteAt = LastActiviteAt;
        LastActiviteAt = DateTime.UtcNow;

        return this;
    }
    public UserSessionState LogOff()
    {
        Name = string.Empty;
        Email = string.Empty;
        PreviousActiviteAt = DateTime.MinValue;
        LastActiviteAt = DateTime.MinValue;
        PreviousPage = CurrentPage = string.Empty;
        IsAuthenticated = false;
        AttendedEvents.Clear();

        return this;
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

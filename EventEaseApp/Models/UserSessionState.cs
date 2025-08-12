using FluentValidation;

namespace EventEaseApp.Models;

public class UserSessionState
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }  = string.Empty;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string CurrentPage { get; set; } = String.Empty;
    public bool IsAuthenticated { get; set; } = false;
    public List<EventModel> AttendedEvents { get; set; } = new();
}

public class UserValidator : AbstractValidator<UserSessionState>
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

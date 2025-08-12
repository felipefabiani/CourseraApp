using FluentValidation;

namespace EventEase.Models;

public class EventModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class EventModelValidator : AbstractValidator<EventModel>
{
    public EventModelValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Event name is required.")
            .MaximumLength(100);

        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(100);

        RuleFor(e => e.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(e => e.Description)
            .MaximumLength(200);
    }
}

using FluentValidation;

namespace CourseraApp.Client.Models;

public class Feedback
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Id:D3}-{(Id+Name+Email+Comment).GetHashCode().ToString("X8")}";
    }
}

public class FeedbackValidator : AbstractValidator<Feedback>
{
    public FeedbackValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");
    }
}
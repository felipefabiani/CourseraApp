using EventEaseApp.Models;
using Microsoft.AspNetCore.Components;

namespace EventEaseApp.Pages.Shared;

public class EventEaseAppBase : ComponentBase
{
    [CascadingParameter(Name = "User")] protected UserSessionState User { get; set; } = default!;

    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        User.UpdatePage(Navigation);
    }
}

using EventEaseApp.Services;
using Microsoft.AspNetCore.Components;

namespace EventEaseApp.Pages.Shared;

public class EventEaseAppBase : ComponentBase
{
    [Inject] protected UserSessionService UserSessionService { get; set; } = default!;

    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await UserSessionService.UpdatePageAsync(Navigation);
    }  
}

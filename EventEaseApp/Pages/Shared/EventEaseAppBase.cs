using EventEaseApp.Models;
using Microsoft.AspNetCore.Components;

namespace EventEaseApp.Pages.Shared;

public class EventEaseAppBase : ComponentBase
{
    [Inject]
    protected UserSessionState User { get; set; } = default!;

    [Inject]
    protected NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        var currentPage = Navigation.Uri
            .Replace(Navigation.BaseUri, "")
            .Replace("/", " ");
        
        if(string.IsNullOrWhiteSpace(currentPage))
        {
            currentPage = "Home";
        }

        User.CurrentPage = currentPage;
    }
}

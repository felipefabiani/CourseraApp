
using Blazored.SessionStorage;
using EventEaseApp.Models;
using Microsoft.AspNetCore.Components;

namespace EventEaseApp.Services;
public class UserSessionService
{
    private const string SessionKey = "UserSession";
    private readonly ISessionStorageService _storage;
    
    public event Func<Task>? OnChangeAsync;

    public UserSessionService(ISessionStorageService storage)
    {
        _storage = storage;
    }

    public async Task<UserSessionState> GetSessionAsync()
    {
        var stored = (await _storage
            .GetItemAsync<UserSessionState>(SessionKey))
            ?? new UserSessionState();
        return stored;
    }
    public async Task UpdatePageAsync(NavigationManager nav)
    {
        await SaveSession(s => s.UpdatePage(nav));
    }
    public async Task LogInAsync(string name, string email, NavigationManager nav)
    {
        await SaveSession(s =>  s.LogIn(name, email, nav));
    }

    public async Task AddAttendedEventAsync(EventModel eventModel)
    {
        await SaveSession(s => s.AttendedEvents.Add(eventModel));
    }
    public async Task LogOutAsync()
    {
        await SaveSession(s => s.LogOff());
    }

    public async Task ClearSessionAsync()
    {
        await SaveSession(s => s = new UserSessionState());
    }

    private async Task SaveSession(Action<UserSessionState> func)
    {
        var stored = await GetSessionAsync();
        func.Invoke(stored);
        await _storage.SetItemAsync(SessionKey, stored);
        await NotifyStateChangedAsync();
    }
    public async Task NotifyStateChangedAsync()
    {
        if (OnChangeAsync?.HasSingleTarget == true)
        {
            await OnChangeAsync.Invoke();
        }
    }
}

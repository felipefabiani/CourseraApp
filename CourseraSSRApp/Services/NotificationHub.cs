using Microsoft.AspNetCore.SignalR;


namespace CourseraSSRApp.Services;
public class NotificationHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendClickCountMessage(int total)
    {
        await Clients.All.SendAsync("ReceivedClickCountMessage", total);
    }
}
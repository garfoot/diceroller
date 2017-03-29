using Microsoft.AspNetCore.SignalR;

namespace DiceRoller.Web.Services.Hubs
{
    public class DiceHub : Hub
    {
        public void Connect(string roomName)
        {
            Clients.Caller.connectedToRoom(roomName, new[]{"Rob", "Andy", "Holly"});
        }
    }
}
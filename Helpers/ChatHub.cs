using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol;
using System.Threading.Tasks;
using Tashtibaat.Models;

[Authorize]
public class ChatHub : Hub
{
    private readonly UserManager<Users> _userManager;

    public ChatHub(UserManager<Users> userManager)
    {
        _userManager = userManager;
    }
    public async Task JoinChat(string userName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId,  userName);
    }
    public async Task SendMessage(string toUsername, string message)
    {
        // Get the user to whom the message should be sent
        var toUser = await _userManager.FindByNameAsync(toUsername);

        if (toUser != null)
        {
            // Get the connection ID of the target user
            var toUserConnectionId = await _userManager.GetUserIdAsync(toUser);
            var x = Groups.ToJson();
            // Send the message to the specific user
            await Clients.Group(toUsername).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
        else
        {
            // Handle the case where the user is not found
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "User not found");
        }
    }


}

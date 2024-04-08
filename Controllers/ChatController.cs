using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Message);
        return Ok();
    }
}

public class ChatMessageDto
{
    public string User { get; set; }
    public string Message { get; set; }
}


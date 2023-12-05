using Application.Chats;
using Application.Interfaces;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public ChatHub(IMediator mediator , IUserAccessor userAccessor)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        public async Task SendChat(Create.Command command)
        {
            var chat = await _mediator.Send(command);

            var groupName = string.Join("_", new[] { _userAccessor.GetUsername(), command.RecipientUsername }.OrderBy(u => u));

            await Clients.Group(groupName).SendAsync("ReceiveChat",JsonConvert.SerializeObject(chat.Value));
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var otherUser = httpContext.Request.Query["username"].ToString();

            var groupName = string.Join("_", new[] { _userAccessor.GetUsername(), otherUser }.OrderBy(u => u));

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);


            var result = await _mediator.Send(new List.Query{RecipientUsername = otherUser});
            

            // await Clients.Group(groupName).SendAsync("ReceiveChat", result.Value);
            await Clients.Caller.SendAsync("LoadChat", JsonConvert.SerializeObject( result.Value));

        }
    }
}
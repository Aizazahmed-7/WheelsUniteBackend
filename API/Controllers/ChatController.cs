using Application.Chats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ChatController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<ChatDTO>>> List()
        {
            return HandleResult(await this.Mediator.Send(new ListCurrent.Query()));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<List<ChatDTO>>> List(string username)
        {
            return  HandleResult(await this.Mediator.Send( new List.Query{RecipientUsername = username}));
        }

        [HttpPost]
        public async Task<ActionResult<ChatDTO>> Create(Create.Command command)
        {
            return HandleResult(await this.Mediator.Send(command));
        }
    }
}
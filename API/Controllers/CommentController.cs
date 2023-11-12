
using Application.Comments;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommentController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateComment(Create.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetComments(Guid postId)
        {
            return HandleResult(await Mediator.Send(new List.Query { PostId = postId }));
        }
    }
}
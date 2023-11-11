
using Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        [HttpPost("{id}")]
        public async Task<IActionResult> Like(Guid id)
        {

            return HandleResult(await Mediator.Send(new Likes.Command { Id = id }));
        }
    }
}
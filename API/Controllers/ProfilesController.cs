using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Username = username }));
        }

        [HttpGet("search/{searchTerm?}")]
        public async Task<IActionResult> SearchProfiles(string searchTerm)
        {
            return HandleResult(await Mediator.Send(new Search.Query { SearchTerm = searchTerm }));
        }

    }
}
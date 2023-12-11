using Application.Events;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    public class EventsController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            return HandleResult(await this.Mediator.Send(new List.Query()));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(string id)
        {
            return HandleResult(await this.Mediator.Send(new Details.Query { Id = Guid.Parse(id) }));
        }

        [HttpGet("{username}/user")]
        public async Task<IActionResult> GetEventsByUser(string username, string predicate)
        {
            return HandleResult(await this.Mediator.Send(new ListByUser.Query { Username = username }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromForm] Create.Command command)
        {
            return HandleResult(await this.Mediator.Send(command));

        }

        [Authorize(Policy = "IsEventHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(Guid id, Event Event)
        {
            Event.Id = id;
            return HandleResult(await this.Mediator.Send(new Edit.Command { Event = Event }));
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            return HandleResult(await this.Mediator.Send(new Delete.Command { Id = Guid.Parse(id) }));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await this.Mediator.Send(new UpdateAttendance.Command { Id = id }));
        }
    }
}
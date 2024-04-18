using Application.Cars;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarsController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return HandleResult(await this.Mediator.Send(new UserCarList.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Add.Command command)
        {
            return HandleResult(await this.Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Car car)
        {
            return HandleResult(await this.Mediator.Send(new Edit.Command { car = car }));
        }

        [HttpPut("{id}/setmain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await this.Mediator.Send(new SetMain.Command { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await this.Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
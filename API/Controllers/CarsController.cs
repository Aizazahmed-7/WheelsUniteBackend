
using Application.Cars;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add(Car car)
        {
            return HandleResult(await Mediator.Send(new Add.Command { car = car }));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Car car)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { car = car }));
        }

        [HttpPut("{id}/setmain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
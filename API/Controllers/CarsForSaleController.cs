

using Application.CarForSale;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarSaleController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCarsForSale()
        {
            return HandleResult(await this.Mediator.Send(new List.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> AddCarForSale(AddCarForSaleDTO carForSale)
        {
            return HandleResult(await this.Mediator.Send(new Add.Command { Object = carForSale }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarForSale(Guid id)
        {
            return HandleResult(await this.Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpGet("mylist")]
        public async Task<IActionResult> GetMyCarsForSale()
        {
            return HandleResult(await this.Mediator.Send(new MyList.Query()));
        }

    }
}
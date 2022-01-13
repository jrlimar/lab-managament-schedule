using LabManagamentSchedule.Core.Mediator;
using LabManagamentSchedule.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckExamController : ControllerBase
    {
        private readonly IMediatorHandler mediatorHandler;

        public CheckExamController(IMediatorHandler mediatorHandler)
        {
            this.mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> SendExamSigned(string dominio, int resultId)
        {
            await mediatorHandler.PublishEvent(new CheckExamEvent(dominio, resultId));

            return Ok();
        }

    }
}

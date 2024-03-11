using BalanceAPI.Services;
using BalanceAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace BalanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{userId}", Name = "current-balance")]
        public async Task<ActionResult<decimal>> CurrentBalance(long userId)
        {
            var currentBalance = await _balanceService.GetUserCurrentBalance(userId);
            return Ok(currentBalance);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut(Name = "update-balance")]
        public async Task<ActionResult> UpdateBalance([FromBody] UpdateBalanceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _balanceService.UpdateBalance(request.UserId, request.Amount);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to update balance.");
            }
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost(Name = "topup-balance")]
        public async Task<ActionResult> TopUpBalance([FromBody] TopUpBalanceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _balanceService.TopUpBalance(request.UserId, request.Amount);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to update balance.");
            }
        }
    }
}

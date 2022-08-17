using Microsoft.AspNetCore.Mvc;
using ATM.Api.Services.Interfaces;
using ATM.Api.Controllers.Requests;
using ATM.Api.Controllers.Responses;

namespace ATM.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/cards")]
    public class AtmController : Controller
    {
        private readonly IBankService _bankService;
        private readonly IAtmService _atmService;

        public AtmController(IBankService bankService, IAtmService atmService)
        {
            _bankService = bankService;
            _atmService = atmService;   
        }

        [HttpGet("{cardNumber}/init")]
        public IActionResult Init([FromRoute] string cardNumber)
        {
            return _bankService.IsCardExist(cardNumber)
                ? Ok(new AtmResponce("Your card is in the system!"))
                : NotFound(new AtmResponce("Your card isn't in the system!"));
        }

        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
        {
            return _bankService.VerifyPassword(request.CardNumber, request.CardPassword)
                ? Ok(new AtmResponce("Your card is in the system!"))
                : Unauthorized(new AtmResponce("Invalid password"!));
        }

        [HttpGet("{cardNumber}/balance")]
        public IActionResult GetBalance([FromRoute] string cardNumber)
        {
            var balance = _bankService.GetCardBalance(cardNumber);

            return Ok(new AtmResponce($"Balance is {balance}$"));
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
        {
            _atmService.Withdraw(request.CardNumber, request.Amount);

            return Ok(new AtmResponce("The operation was successful!"));
        }
    }
}

using ATM.Api.Controllers.Requests;
using ATM.Api.Controllers.Responses;
using ATM.Api.Models;
using ATM.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AtmController : Controller
    {
        private readonly ICardService _cardService;

        public AtmController(ICardService secretaryService)
        {
            _cardService = secretaryService;
        }

        [HttpGet("cards/{cardNumber}/init")]
        public IActionResult Init([FromRoute] string cardNumber)
        {
            if (_cardService.InitCard(cardNumber))
            {
                return Ok(new AtmResponce("Your card is in the system!"));
            }

            return NotFound(new AtmResponce("Your card isn't in the system!"));

        }

        [HttpPost("cards/authorize")]
        public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
        {
            if(_cardService.AuthorizeCard(request.CardNumber, request.CardPassword))
            {
                return Ok(new AtmResponce("Your card is in the system!"));
            }

            return Unauthorized(new AtmResponce("Invalid password"!));
        }

        [HttpGet("cards/{cardNumber}/balance")]
        public IActionResult GetBalance([FromRoute] string cardNumber)
        {
            return _cardService.CheckCardNumber(cardNumber) switch
            {
                { } card => Ok(new AtmResponce($"Balance is {card.GetBalance()}$")),
                _ => NotFound()
            };
        }

        [HttpPut("cards/withdraw")]
        public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
        {
            var card = _cardService.CheckCardNumber(request.CardNumber);

            if (card is null)
            {
                return NotFound();
            }
            if(_cardService.CheckWithdraw(request.CardNumber, request.Amount))
            {
                return Ok(new AtmResponce("The operation was successful!"));
            }

            return BadRequest(new AtmResponce("Invalid data!"));
        }
    }
}

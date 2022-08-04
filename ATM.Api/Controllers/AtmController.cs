using ATM.Api.Controllers.Requests;
using ATM.Api.Controllers.Responses;
using ATM.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AtmController : Controller
    {
        private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };

        [HttpGet("cards/{cardNumber}/init")]
        public IActionResult Init([FromRoute] string cardNumber)
        {
            return Cards.Any(x => x.CardNumber == cardNumber)
                ? Ok(new AtmResponce ("Your card is in the system!"))
                : NotFound(new AtmResponce("Your card isn't in the system!"));
        }

        [HttpPost("cards/authorize")]
        public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == request.CardNumber) switch
            {
                { } card => card.VerifyPassword(request.CardPassword)
                ? Ok(new AtmResponce("Your card is in the system!"))
                : Unauthorized(new AtmResponce("Invalid password")),
              _ => NotFound()
            };
        }

        [HttpGet("cards/{cardNumber}/balance")]
        public IActionResult GetBalance([FromRoute] string cardNumber)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber) switch
            {
                { } card => Ok(new AtmResponce($"Balance is {card.GetBalance()}$")),
                _ => NotFound()
            };
        }

        [HttpPut("cards/withdraw")]
        public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
        {
            var card = Cards.SingleOrDefault(x => x.CardNumber == request.CardNumber);

            if (card is null)
            {
                return NotFound();
            }

            card.Withdraw(request.Amount);

            return Ok(new AtmResponce("The operation was successful!"));
        }
    }
}

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

        public sealed record AuthorizeMode (string CardPassword);

        [HttpGet("cards/{cardNumber}/init")]
        public IActionResult Init([FromRoute] string cardNumber)
        {
            return Cards.Any(x => x.CardNumber == cardNumber)
                ? Ok(new AtmResponce ("Your card is in the system!"))
                : NotFound(new AtmResponce("Your card isn't in the system!"));
        }

        [HttpPost("cards/{cardNumber}/authorize")]
        public IActionResult Authorize([FromRoute] string cardNumber, [FromBody] AuthorizeMode model)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber) switch
            {
                { } card => card.VerifyPassword(model.CardPassword)
                ? Ok(new AtmResponce("Your card is in the system!"))
                : Unauthorized(new AtmResponce("Invalid password")),
              _ => NotFound()
            };
        }

        [HttpGet("cards/{cardNumber}/getBalance")]
        public IActionResult GetBalance([FromRoute] string cardNumber)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber) switch
            {
                { } card => Ok(new AtmResponce($"Balance is {card.GetBalance()}$")),
                _ => NotFound()
            };
        }

        [HttpPut("cards/{cardNumber}/withdraw/{sum}")]
        public IActionResult Withdraw(string cardNumber, int sum)
        {
            var card = Cards.SingleOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
            {
                return NotFound();
            }

            card.Withdraw(sum);

            return Ok(new AtmResponce("The operation was successful!"));
        }
    }
}

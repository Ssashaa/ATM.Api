using ATM.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : Controller
    {
        private readonly ILogger<CardController> _logger;

        public CardController(ILogger<CardController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Init")]
        public IActionResult Init(string cardNumber)
        {
            if (Card.Cards.Any(x => x.CardNumber == cardNumber))
            {
                return Ok("Your card is in the system!");
            }

            return BadRequest("Your card is not in the system!");
        }

        [HttpPost("Authorize")]
        public IActionResult Authorize(string cardNumber, string password)
        {
            if (Card.Cards.Any(x => x.CardNumber == cardNumber && x.Password == password))
            {
                return Ok("Your card is in the system!");
            }

            return BadRequest("Invalid password");
        }

        [HttpGet("Balance")]
        public IActionResult GetBalance (string cardNumber)
        {
            var balance = Card.Cards.Where(x => x.CardNumber == cardNumber).Select(x => x.Balance);
            return Ok(balance);
        }

        [HttpPut("Withdraw")]
        public IActionResult Withdraw(string cardNumber, int sum)
        {
            var balance = Card.Cards.Where(x => x.CardNumber == cardNumber).First();
            if (balance.Balance >= sum)
            {
                balance.Balance -= sum;
                return Ok("The operation was successful!");
            }
            return BadRequest("Insufficient funds!");
        }
    }
}

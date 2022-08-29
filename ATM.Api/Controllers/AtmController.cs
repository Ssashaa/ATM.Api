using ATM.Api.Extentions;
using Microsoft.AspNetCore.Mvc;
using ATM.Api.Services.Interfaces;
using ATM.Api.Controllers.Requests;
using ATM.Api.Controllers.Responses;

namespace ATM.Api.Controllers;

[ApiController]
[Route("/api/[controller]/cards")]
public class AtmController : Controller
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService)
    {
        _atmService = atmService;
    }

    [HttpGet("{cardNumber}/init", Name =nameof(Init))]
    public IActionResult Init([FromServices] AtmLinkGenerator linkGenerator, [FromRoute] string cardNumber)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Init));

        return _atmService.IsCardExist(cardNumber)
            ? Ok(new AtmResponce($"Your card in the system!", links))
            : NotFound(new AtmResponce("Your card isn't in the system!"));
    }

    [HttpPost("authorize", Name =nameof(Authorize))]
    public IActionResult Authorize([FromServices] AtmLinkGenerator linkGenerator, [FromBody] CardAuthorizeRequest request)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Authorize), new { request.CardNumber });

        return _atmService.VerifyPassword(request.CardNumber, request.CardPassword)
            ? Ok(new AtmResponce("Your card is in the system!", links))
            : Unauthorized(new AtmResponce("Invalid password"!));
    }

    [HttpGet("{cardNumber}/balance", Name = nameof(GetBalance))]
    public IActionResult GetBalance([FromServices] AtmLinkGenerator linkGenerator, [FromRoute] string cardNumber)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(GetBalance));

        var balance = _atmService.GetCardBalance(cardNumber);

        return Ok(new AtmResponce($"Balance is {balance}$", links));
    }

    [HttpPost("withdraw", Name = nameof(Withdraw))]
    public IActionResult Withdraw([FromServices] AtmLinkGenerator linkGenerator, [FromBody] CardWithdrawRequest request)
    {
        var links = linkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Withdraw));

        _atmService.Withdraw(request.CardNumber, request.Amount);

        return Ok(new AtmResponce("The operation was successful!", links));
    }
}

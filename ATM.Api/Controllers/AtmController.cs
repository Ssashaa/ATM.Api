using Microsoft.AspNetCore.Mvc;
using ATM.Api.Services.Interfaces;
using ATM.Api.Controllers.Requests;
using ATM.Api.Controllers.Responses;
using ATM.Api.Services;

namespace ATM.Api.Controllers;

[ApiController]
[Route("/api/[controller]/cards")]
public class AtmController : Controller
{
    private readonly IAtmService _atmService;
    private readonly IAtmLinkGenerator _atmLinkGenerator;

    public AtmController(IAtmService atmService, IAtmLinkGenerator atmLinkGenerator)
    {
        _atmService = atmService;
        _atmLinkGenerator = atmLinkGenerator;
    }

    [HttpGet("{cardNumber}/init", Name =nameof(Init))]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Init));

        return _atmService.IsCardExist(cardNumber)
            ? Ok(new AtmResponce($"Your card in the system!", links))
            : NotFound(new AtmResponce("Your card isn't in the system!"));
    }

    [HttpPost("authorize", Name =nameof(Authorize))]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Authorize), new { request.CardNumber });

        return _atmService.VerifyPassword(request.CardNumber, request.CardPassword)
            ? Ok(new AtmResponce("Your card is in the system!", links))
            : Unauthorized(new AtmResponce("Invalid password"!));
    }

    [HttpGet("{cardNumber}/balance", Name = nameof(GetBalance))]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(GetBalance));

        var balance = _atmService.GetCardBalance(cardNumber);

        return Ok(new AtmResponce($"Balance is {balance}$", links));
    }

    [HttpPost("withdraw", Name = nameof(Withdraw))]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Withdraw));

        _atmService.Withdraw(request.CardNumber, request.Amount);

        return Ok(new AtmResponce("The operation was successful!", links));
    }
}

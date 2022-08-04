namespace ATM.Api.Controllers.Requests
{
    public sealed record CardAuthorizeRequest(string CardNumber, string CardPassword);
}

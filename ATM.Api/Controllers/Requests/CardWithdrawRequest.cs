namespace ATM.Api.Controllers.Requests
{
    public sealed record CardWithdrawRequest(string CardNumber, decimal Amount);
}

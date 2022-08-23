namespace ATM.Api.Services.Interfaces;

public interface IAtmService
{
    public bool IsCardExist(string cardNumber);

    public bool VerifyPassword(string cardNumber, string cardPassword);

    public decimal GetCardBalance(string cardNumber);

    public void Withdraw(string cardNumber, decimal amount);
}

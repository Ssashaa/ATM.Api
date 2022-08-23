using ATM.Api.Models;

namespace ATM.Api.Services.Interfaces
{
    public interface IBankService
    {
        public bool IsCardExist(string cardNumber);

        public bool VerifyPassword(string cardNumber, string cardPassword);

        public void Withdraw(string cardNumber, decimal amount);

        public decimal GetCardBalance(string cardNumber);

        public Card GetCard(string cardNumber);
    }
}

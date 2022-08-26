using ATM.Api.Models.Events;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services
{
    public sealed class AtmService : IAtmService
    {
        private readonly IBankService _bankService;
        public decimal TotalAmount { get; set; } = 10_000;

        public AtmService(IBankService bankService)
        {
            _bankService = bankService;
        }

        public bool IsCardExist (string cardNumber) =>
            _bankService.IsCardExist (cardNumber);

        public bool VerifyPassword(string cardNumber, string cardPassword) =>
            _bankService.VerifyPassword(cardNumber, cardPassword);

        public decimal GetCardBalance(string cardNumber) =>
            _bankService.GetCardBalance(cardNumber);
        

        public void Withdraw(string cardNumber, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid amount entered!");
            }

            if (amount > TotalAmount)
            {
                throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
            }

            _bankService.Withdraw(cardNumber, amount);

            TotalAmount -= amount;
        }
    }
}

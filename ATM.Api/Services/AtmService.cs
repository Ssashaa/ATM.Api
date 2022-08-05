using ATM.Api.Models;
using ATM.Api.Service.Interfaces;

namespace ATM.Api.Services
{
    public sealed class AtmService : IAtmService
    {
        private decimal TotalAmount { get; set; } = 10_000;

        private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };

        public bool IsCardExist(string cardNumber) => Cards.Any(x => x.CardNumber == cardNumber);

        public decimal GetCardBalance(string cardNumber)
            => GetCard(cardNumber)
            .GetBalance();

        public bool VerifyPassword(string cardNumber, string cardPassword) 
            => GetCard(cardNumber)
            .IsPasswordEqual(cardPassword);

        private Card GetCard (string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

        public void Withdraw(string cardNumber, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Invalid amount entered!");
            }

            if (amount > TotalAmount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Insufficient funds at the ATM!");
            }

            GetCard(cardNumber).Withdraw(amount);

            TotalAmount -= amount;
        }
    }
}

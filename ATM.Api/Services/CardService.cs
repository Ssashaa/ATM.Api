using ATM.Api.Controllers.Responses;
using ATM.Api.Models;
using ATM.Api.Service.Interfaces;

namespace ATM.Api.Services
{
    public class CardService : ICardService
    {
        private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };

        private static Atm atm = new Atm(1000);

        public bool InitCard(string cardNumber)
        {
            return Cards.Any(x => x.CardNumber == cardNumber);
        }


        public bool AuthorizeCard(string cardNumber, string cardPassword)
        {
            return CheckCardNumber(cardNumber) switch
            {
                { } card => card.VerifyPassword(cardPassword)
                ? true
                : false
            };
        }

        public Card CheckCardNumber (string cardNumber)
        {
            return Cards.SingleOrDefault(x => x.CardNumber == cardNumber);
        }

        public bool CheckWithdraw(string cardNumber, decimal amount)
        {
            var card = CheckCardNumber(cardNumber);
            if(card.Balance < amount)
            {
                throw new ArgumentOutOfRangeException("The balance on your card is less than the amount!");
            }
            if(amount < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid amount entered!");
            }
            if(atm.TotalAmount < amount)
            {
                throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
            }

            atm.WithdrawAtmAmount(amount);
            card.Withdraw(amount);

            return true;
        }
    }
}

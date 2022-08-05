using ATM.Api.Models;

namespace ATM.Api.Service.Interfaces
{
    public interface ICardService
    {
        public bool InitCard(string cardNumber);

        public bool AuthorizeCard(string cardNumber, string cardPassword);

        public Card CheckCardNumber(string cardNumber);

        public bool CheckWithdraw(string cardNumber, decimal amount);
    }
}

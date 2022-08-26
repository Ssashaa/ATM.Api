using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services
{
    public sealed class AtmEventService : IAtmService
    {
        private readonly IAtmService _atm;
        private readonly IAtmEventBroker _broker;

        public AtmEventService(IAtmService atm, IAtmEventBroker broker) => (_atm, _broker) = (atm, broker);

        public decimal GetCardBalance(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsCardExist(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string cardNumber, string cardPassword)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(string cardNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}

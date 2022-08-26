using ATM.Api.Models.Events;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services
{
    public sealed class AtmEventService : IAtmService
    {
        private readonly IAtmService _atm;
        private readonly IAtmEventBroker _broker;

        public AtmEventService(IAtmService atm, IAtmEventBroker broker) => (_atm, _broker) = (atm, broker);

        public bool IsCardExist(string cardNumber)
        {
            if (_atm.IsCardExist(cardNumber))
            {
                _broker.StartStream(cardNumber, new AtmEvent());
                _broker.AppendEvent(cardNumber, new CardInit());
                return true;
            }

            throw new UnauthorizedAccessException("Pass identification and authorization!");
        }

        public bool VerifyPassword(string cardNumber, string cardPassword)
        {
            var @event = _broker.FindEvent<CardInit>(cardNumber);

            if (@event is not { })
            {
                throw new UnauthorizedAccessException("Pass identification!");
            }

            if (_atm.VerifyPassword(cardNumber, cardPassword))
            {
                _broker.AppendEvent(cardNumber, new CardAuthorized());

                return true;
            }

            return false;
        }

        public decimal GetCardBalance(string cardNumber)
        {
            var @event = _broker.GetLastEvent(cardNumber);

            if (@event is not CardAuthorized)
            {
                throw new UnauthorizedAccessException("Pass identification and authorization!");
            }

            _broker.AppendEvent(cardNumber, new BalanceEvent());

            return _atm.GetCardBalance(cardNumber);
        }

        public void Withdraw(string cardNumber, decimal amount)
        {
            var @event = _broker.GetLastEvent(cardNumber);

            if (@event is not CardAuthorized)
            {
                throw new UnauthorizedAccessException("Pass identification and authorization!");
            }

            _broker.AppendEvent(cardNumber, new WithdrawEvent());

            _atm.Withdraw(cardNumber, amount);
        }
    }
}

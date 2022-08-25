using ATM.Api.Models.Events;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services
{
    public sealed class AtmService : IAtmService
    {
        private readonly IBankService _bankService;
        private IAtmEventBroker _broker;
        public decimal TotalAmount { get; set; } = 10_000;

        public AtmService(IBankService bankService, IAtmEventBroker broker)
        {
            _bankService = bankService;
            _broker = broker;   
        }

        public bool IsCardExist(string cardNumber)
        {
            if (_bankService.IsCardExist(cardNumber))
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

            if(_bankService.VerifyPassword(cardNumber, cardPassword))
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
                throw new InvalidOperationException("Could not perform unauthorized operation");
            }

            _broker.AppendEvent(cardNumber, new BalanceEvent());

            return _bankService.GetCardBalance(cardNumber);
        }

        public void Withdraw(string cardNumber, decimal amount)
        {
            var @event = _broker.GetLastEvent(cardNumber);

            if (@event is not CardAuthorized)
            {
                throw new UnauthorizedAccessException("Pass identification and authorization!");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid amount entered!");
            }

            if (amount > TotalAmount)
            {
                throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
            }

            _broker.AppendEvent(cardNumber, new WithdrawEvent());

            _bankService.Withdraw(cardNumber, amount);

            TotalAmount -= amount;
        }
    }
}

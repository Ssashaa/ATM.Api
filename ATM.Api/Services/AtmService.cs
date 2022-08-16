using ATM.Api.Models;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services
{
    public sealed class AtmService : IAtmService
    {
        public decimal TotalAmount { get; set; } = 10_000;

        public void WithdrawAtm(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid amount entered!");
            }

            if (amount > TotalAmount)
            {
                throw new ArgumentOutOfRangeException("Insufficient funds at the ATM!");
            }

            TotalAmount -= amount;
        }
    }
}

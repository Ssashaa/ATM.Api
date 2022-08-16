using ATM.Api.Models;

namespace ATM.Api.Services.Interfaces
{
    public interface IAtmService
    {
        public void WithdrawAtm(decimal amount);
    }
}

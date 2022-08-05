namespace ATM.Api.Models
{
    public class Atm
    {
        public decimal TotalAmount { get; set; }

        public Atm (decimal totalAmount)
        {
            TotalAmount = totalAmount;
        }

        public decimal WithdrawAtmAmount(decimal amount) => TotalAmount -= amount;

    }
}

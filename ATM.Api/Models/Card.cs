namespace ATM.Api.Models
{
    public class Card
    {
        public string CardNumber { get; }

        public string Owner { get; }

        public string Password { get; }

        public CardBrands Brand { get; }

        private decimal Balance { get; set; }

        public Card(string cardNumber, string owner, string password, CardBrands brand, decimal balance)
        {
            CardNumber = cardNumber;
            Owner = owner;
            Password = password;
            Brand = brand;
            Balance = balance;
        }

        public bool IsPasswordEqual(string cardPassword) => cardPassword == Password;

        public decimal GetBalance() => Balance;

        public void Withdraw(decimal amount)
        {
            if (amount > GetBalance())
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The balance on your card is less than the amount!");
            }

            Balance -= amount;
        }
    }
}

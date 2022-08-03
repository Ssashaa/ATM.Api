namespace ATM.Api.Models
{
    public class Card
    {
        public string CardNumber { get; }

        public string Owner { get; }

        public string Password { get; }

        public CardBrands Brand { get; }

        public decimal Balance { get; set; }

        public Card(string cardNumber, string owner, string password, CardBrands brand, decimal balance)
        {
            CardNumber = cardNumber;
            Owner = owner;
            Password = password;
            Brand = brand;
            Balance = balance;
        }

        public bool VerifyPassword (string cardPassword) => cardPassword == Password;

        public decimal GetBalance() => Balance;

        public decimal Withdraw(decimal amount) => Balance -= amount;
    }
}

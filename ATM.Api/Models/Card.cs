namespace ATM.Api.Models
{
    public class Card
    {
        public string CardNumber { get; set; }

        public string Owner { get; set; }

        public string Password { get; set; }

        public CardBrands Brand { get; set; }

        public decimal Balance { get; set; }

        public Card(string cardNumber, string owner, string password, CardBrands brand, decimal balance)
        {
            CardNumber = cardNumber;
            Owner = owner;
            Password = password;
            Brand = brand;
            Balance = balance;
        }
    }
}

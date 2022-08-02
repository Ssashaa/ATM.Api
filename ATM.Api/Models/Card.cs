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

        public static List<Card> Cards = new()
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };
    }
}

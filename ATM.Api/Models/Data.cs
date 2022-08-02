namespace ATM.Api.Models
{
    public class Data
    {
        public static List<Card> Cards = new()
        {
            new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
            new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
        };
    }
}

using ATM.Api.Models;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services;

public class BankService : IBankService
{
    private const int LimitVisa = 200;
    private const int LimitMasterCard = 300;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.CardNumber == cardNumber);

    public decimal GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
        .GetBalance();

    public bool VerifyPassword(string cardNumber, string cardPassword)
        => GetCard(cardNumber)
        .IsPasswordEqual(cardPassword);

    public Card GetCard(string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

    public bool VerifyCardLimit(string cardNumber, decimal amount)
    {
        var card = GetCard(cardNumber);

        return (card.Brand, amount) switch
        {
            { Brand: CardBrands.Visa, amount: > LimitVisa } =>
                throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {LimitVisa}"),
            { Brand: CardBrands.MasterCard, amount: > LimitMasterCard } =>
                throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {LimitMasterCard}"),
            _ => true
        };
    }
}

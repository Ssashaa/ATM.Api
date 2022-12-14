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

    private static readonly IReadOnlyCollection<CardBrandLimit> WithdrawLimits = new List<CardBrandLimit>
    {
        new (CardBrands.Visa, LimitVisa),
        new (CardBrands.MasterCard, LimitMasterCard)
    };

    private static decimal GetWithdrawLimit(CardBrands cardBrand)
    {
        return WithdrawLimits.First(x => x.CardBrand == cardBrand).Amount;
    }

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.CardNumber == cardNumber);

    public decimal GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
        .GetBalance();

    public bool VerifyPassword(string cardNumber, string cardPassword)
        =>GetCard(cardNumber)
        .IsPasswordEqual(cardPassword);

    public Card GetCard(string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

    public void Withdraw (string cardNumber, decimal amount)
    {
        var card = GetCard(cardNumber);
        var limit = GetWithdrawLimit(card.Brand);

        if(amount > limit)
        {
            throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {limit}");
        }

        card.Withdraw(amount);
    }
}

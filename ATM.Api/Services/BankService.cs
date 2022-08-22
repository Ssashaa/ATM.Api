using ATM.Api.Models;
using ATM.Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ATM.Api.Services;

public class BankService : IBankService
{
    private const string initKey = "init";
    private const string authorizeKey = "author";
    private const int LimitVisa = 200;
    private const int LimitMasterCard = 300;
    private IMemoryCache _cache;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    private static readonly IReadOnlyCollection<CardBrandLimit> WithdrawLimits = new List<CardBrandLimit>
    {
        new (CardBrands.Visa, 200),
        new (CardBrands.MasterCard, 300)
    };

    public BankService (IMemoryCache cache)
    {
        _cache = cache;
    }

    private static decimal GetWithdrawLimit(CardBrands cardBrand)
    {
        return WithdrawLimits.First(x => x.CardBrand == cardBrand).Amount;
    }

    public bool IsCardExist(string cardNumber)
    {
        if (Cards.Any(x => x.CardNumber == cardNumber))
        {
            _cache.Set(initKey, cardNumber);
            return true;
        }

        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public decimal GetCardBalance(string cardNumber)
    {
        if (_cache.TryGetValue(authorizeKey, out string token))
        {
            _cache.Remove(authorizeKey);
            return GetCard(cardNumber).GetBalance();
        }

        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        if (_cache.TryGetValue(initKey, out string token) && GetCard(cardNumber).IsPasswordEqual(cardPassword))
        {
            _cache.Remove(initKey);
            _cache.Set(authorizeKey, cardPassword);
            return true;
        }

        throw new InvalidOperationException("Pass identification and authorization!");
    }

    public Card GetCard(string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

    //public bool VerifyCardLimit(string cardNumber, decimal amount)
    //{
    //    var card = GetCard(cardNumber);

    //    if (_cache.TryGetValue(authorizeKey, out string token))
    //    {
    //        _cache.Remove(authorizeKey);

    //        return (card.Brand, amount) switch
    //        {
    //            { Brand: CardBrands.Visa, amount: > LimitVisa } =>
    //                throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {LimitVisa}"),
    //            { Brand: CardBrands.MasterCard, amount: > LimitMasterCard } =>
    //                throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {LimitMasterCard}"),
    //            _ => true
    //        };
    //    }

    //    throw new InvalidOperationException("Pass identification and authorization!");
    //}

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

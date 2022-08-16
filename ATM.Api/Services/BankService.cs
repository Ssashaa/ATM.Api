using ATM.Api.Models;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Services;

public class BankService : IBankService
{
    private const int limitVisa = 200;
    private const int limitMasterCard = 300;
    private readonly IAtmService _atmService;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public BankService (IAtmService atmService)
    {
        _atmService = atmService;
    }

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.CardNumber == cardNumber);

    public decimal GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
        .GetBalance();

    public bool VerifyPassword(string cardNumber, string cardPassword)
        => GetCard(cardNumber)
        .IsPasswordEqual(cardPassword);

    private Card GetCard(string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

    public void Withdraw(string cardNumber, decimal amount)
    {
        var card = GetCard(cardNumber);
        
        if(card.Brand == CardBrands.MasterCard && amount > limitMasterCard)
        {
            throw new InvalidOperationException($"One time { card.Brand } withdraw limit is {limitMasterCard}");
        }

        if (card.Brand == CardBrands.Visa && amount > limitVisa)
        {
            throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {limitVisa}");
        }

        _atmService.WithdrawAtm(amount);
        card.Withdraw(amount);
    }
}

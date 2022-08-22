namespace ATM.Api.Models;

public sealed record CardBrandLimit(
    CardBrands CardBrand,
    decimal Amount);
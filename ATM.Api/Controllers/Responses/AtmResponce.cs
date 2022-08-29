using ATM.Api.Controllers.Common;

namespace ATM.Api.Controllers.Responses;

public sealed record AtmResponce(string Message)
{
    public ApiEndpoint[] Links { get; init; } = Array.Empty<ApiEndpoint>();

    public AtmResponce(string message, ApiEndpoint[] links) : this(message) => Links = links;
}

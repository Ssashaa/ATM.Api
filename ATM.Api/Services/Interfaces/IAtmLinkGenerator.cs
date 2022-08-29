using ATM.Api.Controllers.Common;

namespace ATM.Api.Services.Interfaces;

public interface IAtmLinkGenerator
{
    public ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null);
}

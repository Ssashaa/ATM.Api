using ATM.Api.Controllers.Common;

namespace ATM.Api.Extentions;

public static class LinkGeneratorExtensions
{
    public static ApiEndpoint GetAssociatedEndpoint(
        this LinkGenerator linkGenerator,
        HttpContext httpContext,
        HttpMethod httpMethod,
        string endpointName,
        object? values = null)
    {
        var link = linkGenerator.GetUriByName(httpContext, endpointName, values);

        return new (endpointName, link, httpMethod.Method);
    }
}
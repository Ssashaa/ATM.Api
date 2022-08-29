using ATM.Api.Controllers;
using ATM.Api.Controllers.Common;

namespace ATM.Api.Extentions
{

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

    public sealed class AtmLinkGenerator
    {
        private readonly LinkGenerator _linkGenerator;

        public AtmLinkGenerator(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;

        public ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null)
        {
            return endpointName switch
            {
                nameof(AtmController.Init) => new[]
                {
                    _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Authorize))
                },
                nameof(AtmController.Authorize) => new[]
                {
                    _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Get, nameof(AtmController.GetBalance), values),
                    _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Withdraw))
                },
                _ => throw new ArgumentOutOfRangeException("Invalid data!")
            };
        }
    }
}

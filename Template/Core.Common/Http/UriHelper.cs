namespace Core.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class UriHelper
    {
        public static UriBuilder Build(Uri baseUri, string route, params string[] parameters)
        {
            var modifiedRoute = ValidateAndGetModifiedRoute(baseUri, route, parameters);
            return new UriBuilder(baseUri) { Path = modifiedRoute };
        }

        public static UriBuilder BuildWithPath(Uri baseUri, string route, params string[] parameters)
        {
            var modifiedRoute = ValidateAndGetModifiedRoute(baseUri, route, parameters);
            var builder = new UriBuilder(baseUri);
            var path = builder.Path;
            var modifiedPath = string.IsNullOrWhiteSpace(path) ? modifiedRoute : path.TrimEnd('/') + '/' + modifiedRoute.TrimStart('/');

            builder.Path = modifiedPath;
            return builder;
        }

        private static string ValidateAndGetModifiedRoute(Uri baseUri, string route, IEnumerable<string> parameters)
        {
            var modifiedRoute = route;

            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }

            if (string.IsNullOrEmpty(route))
            {
                throw new ArgumentNullException("route");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            foreach (var parameter in parameters)
            {
                var m = "";
                var match = Regex.Match(modifiedRoute, @"\{\w*\}");
                if (!match.Success)
                {
                    throw new InvalidOperationException("Route contains fewer parameters than were specified.");
                }

                modifiedRoute = modifiedRoute.Replace(modifiedRoute.Substring(match.Index, match.Length), parameter);
            }

            if (Regex.Match(modifiedRoute, @"\{\w*\}").Success)
            {
                throw new InvalidOperationException("Route contains more parameters than were specified.");
            }
            return modifiedRoute;
        }
    }
}

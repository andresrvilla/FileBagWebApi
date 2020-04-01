using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FileBagWebApi.Infraestructure
{
    public static class HttpRequestHeadersExtensions
    {
        private const string AUTHORIZATION_KEY = "Authorization";

        private const string BEARER_KEY = "Bearer";

        public static string GetToken(this IHeaderDictionary me)
        {
            if (me.ContainsKey(AUTHORIZATION_KEY))
            {
                var value = me[AUTHORIZATION_KEY].ToString();

                if (value.Contains(BEARER_KEY))
                {
                    return value.Replace(BEARER_KEY, string.Empty).Trim();
                }
            }
            return string.Empty;
        }

        public static string GetToken(this HttpHeaders me)
        {
            if (me.Contains(AUTHORIZATION_KEY))
            {
                var values = me.GetValues(AUTHORIZATION_KEY);

                foreach (var value in values)
                {
                    if (value.Contains(BEARER_KEY))
                    {
                        return value.Replace(BEARER_KEY, string.Empty).Trim();
                    }
                }
            }
            return string.Empty;
        }
    }
}

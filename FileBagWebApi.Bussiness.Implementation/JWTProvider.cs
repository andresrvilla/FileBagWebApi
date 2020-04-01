using FileBagWebApi.Bussiness.Interfaces;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Bussiness.Implementation
{
    public class JWTProvider : ITokenProvider
    {
        private double _expirationInSeconds;

        public JWTProvider(double expirationInSeconds)
        {
            _expirationInSeconds = expirationInSeconds;
        }

        public IDictionary<string, object> Decode(string token, string secret)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var payload = decoder.DecodeToObject<IDictionary<string, object>>(token, secret, verify: true);
                return payload;
            }
            catch (TokenExpiredException ex)
            {
                Console.WriteLine("Token has expired");
                throw ex;
            }
            catch (SignatureVerificationException ex2)
            {
                Console.WriteLine("Token has invalid signature");
                throw ex2;
            }
        }

        public string Generate(IDictionary<string, object> payload, string secret)
        {
            string result;

            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();

            var secondsSinceEpoch = UnixEpoch.GetSecondsSince(now.AddSeconds(_expirationInSeconds));

            payload.Add("exp", secondsSinceEpoch);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            result = encoder.Encode(payload, secret);
            return result;
        }
    }
}

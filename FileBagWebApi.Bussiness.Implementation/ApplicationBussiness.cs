using CSharpUtilities.Extensions;
using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileBagWebApi.Bussiness.Implementation
{
    public class ApplicationBussiness : IApplicationBussiness
    {
        public IApplicationDataAccess _dataAccess { get; set; }

        public ITokenProvider _tokenProvider { get; set; }

        private IInMemoryCache _memoryCache;

        private string _cypherKey;

        public ApplicationBussiness(IApplicationDataAccess dataAccess, ITokenProvider tokenProvider, IInMemoryCache memoryCache, string cypherKey)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException("dataAccess");
            }

            if (string.IsNullOrWhiteSpace(cypherKey))
            {
                throw new ArgumentNullException(cypherKey);
            }

            _dataAccess = dataAccess;
            _tokenProvider = tokenProvider;
            _memoryCache = memoryCache;
            _cypherKey = cypherKey;
        }

        public async Task<Application> Register(string name, string secret, string URI)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }

            if (string.IsNullOrWhiteSpace(URI))
            {
                throw new ArgumentException("URI");
            }

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new ArgumentException("secret");
            }

            return await _dataAccess.Register(name, secret.Encrypt(_cypherKey), URI);
        }

        public async Task<bool> Exists(string id)
        {
            Guid guid = id.ToGuid();
            return await _dataAccess.Exists(guid);
        }

        public async Task<Application> GetByIdAndSecret(string id, string secret)
        {
            Guid guid = id.ToGuid();

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new ArgumentException("secret");
            }
            var app = await _dataAccess.GetById(guid);
            if (app != null && app.Secret.Decrypt(_cypherKey) != secret)
            {
                app = null;
            }

            return app;
        }

        public async Task<Application> GetById(string id)
        {
            Guid guid = id.ToGuid();
            return await _dataAccess.GetById(guid);
        }

        public async Task UpdateToken(string id, string token)
        {
            Guid guid = id.ToGuid();

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("token");
            }

            await _dataAccess.UpdateToken(guid, token);
        }

        public async Task RemoveToken(string id)
        {
            Guid guid = id.ToGuid();
            await _dataAccess.RemoveToken(guid);
        }

        public async Task<string> GetToken(string id, string secret)
        {
            string result = string.Empty;
            var app = await GetByIdAndSecret(id, secret);
            if (app != null)
            {
                var payload = new Dictionary<string, object>
                {
                    { "uid", app.Id }
                };

                result = _tokenProvider.Generate(payload,_cypherKey);
            }
            return result;
        }


        public async Task<Application> GetByToken(string token)
        {
            //TODO: Cache?
            //var payload = _tokenProvider.Decode(token,_cypherKey);
            //return await GetById(payload["uid"].ToString());


            Application result = null;
            var payload = _tokenProvider.Decode(token, _cypherKey);
            string uid = payload["uid"].ToString();
            string currentKey = string.Format(ApplicationBussinessConstants.GETBYTOKEN_CACHEKEY, uid);
            if (_memoryCache.TryGetValue(currentKey, out result))
            {
                return result;
            }
            else
            {
                var app = await GetById(uid);
                _memoryCache.Set<bool>(currentKey, app, InMemoryCacheOffset.FiveMinutes);
                return app;
            }

        }
    }

    class ApplicationBussinessConstants
    {
        public const string GETBYTOKEN_CACHEKEY = "GetByToken_{0}";
    }
}

using CSharpUtilities.Net.Rest.DTOClasses;
using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FileBagWebApi.Infraestructure
{
    public class ServiceTokenValidator : IServiceTokenValidator
    {
        private readonly IApplicationBussiness _applicationBussiness;

        private const string AUTHORIZATION_KEY = "Authorization";

        private const string BEARER_KEY = "Bearer";

        public ServiceTokenValidator(IApplicationBussiness applicationBussiness)
        {
            _applicationBussiness = applicationBussiness;
        }

        public ObjectResult Validate(IHeaderDictionary headerDictionary)
        {
            string token = headerDictionary.GetToken();
            if (IsValidToken(token))
            {
                return new ObjectResult(new RestErrorResponseDTO(StatusCodes.Status403Forbidden, "Invalid Token")) { StatusCode = StatusCodes.Status403Forbidden };
            }
            else
            {
                return null;
            }
        }

        public bool Validate(HttpHeaders headerDictionary)
        {
            string token = headerDictionary.GetToken();
            return IsValidToken(token);
        }

        private bool IsValidToken(string token)
        {
            try
            {
                bool isError = true;

                Application result = Task.Run(() => _applicationBussiness.GetByToken(token)).Result;
                if (result != null)
                {
                    isError = false;
                }

                return isError;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

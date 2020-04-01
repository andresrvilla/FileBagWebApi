using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace FileBagWebApi.Infraestructure
{
    public interface IServiceTokenValidator
    {
        ObjectResult Validate(IHeaderDictionary headerDictionary);

        bool Validate(HttpHeaders headerDictionary);
    }
}
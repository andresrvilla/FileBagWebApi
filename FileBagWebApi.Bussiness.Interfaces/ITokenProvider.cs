using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Bussiness.Interfaces
{
    public interface ITokenProvider
    {
        string Generate(IDictionary<string, object> payload, string secret);

        IDictionary<string, object> Decode(string token, string secret);
    }
}

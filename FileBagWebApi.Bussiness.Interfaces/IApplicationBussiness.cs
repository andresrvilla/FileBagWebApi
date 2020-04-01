using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Models;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.Bussiness.Interfaces
{
    public interface IApplicationBussiness
    {
        IApplicationDataAccess _dataAccess { get; set; }

        Task<Application> Register(string name, string secret, string URI);

        Task<bool> Exists(string id);

        Task<Application> GetByIdAndSecret(string id, string secret);

        Task<Application> GetById(string id);

        Task UpdateToken(string id, string token);

        Task RemoveToken(string id);

        Task<string> GetToken(string id, string secret);

        Task<Application> GetByToken(string token);
    }
}

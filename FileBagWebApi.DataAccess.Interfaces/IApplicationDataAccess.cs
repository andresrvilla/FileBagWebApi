using FileBagWebApi.Entities.Models;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.DataAccess.Interfaces
{
    public interface IApplicationDataAccess
    {
        Task<Application> Register(string name, string secret, string URI);

        Task<bool> Exists(Guid id);

        Task<Application> GetByIdAndSecret(Guid id, string secret);

        Task<Application> GetById(Guid id);

        Task UpdateToken(Guid id, string token);

        Task RemoveToken(Guid id);

        
    }
}

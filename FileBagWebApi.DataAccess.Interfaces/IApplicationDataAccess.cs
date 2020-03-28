using FileBagWebApi.Entities.Models;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.DataAccess.Interfaces
{
    public interface IApplicationDataAccess
    {
        Task<Application> Register(string name, string URI);

        Task<bool> Exists(Guid id);
    }
}

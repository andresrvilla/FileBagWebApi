using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Models;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.Bussiness.Interfaces
{
    public interface IApplicationBussiness
    {
        IApplicationDataAccess _dataAccess { get; set; }

        Task<Application> Register(string name, string URI);

        Task<bool> Exists(string id);
    }
}

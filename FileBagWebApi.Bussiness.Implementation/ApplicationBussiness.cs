using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Models;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.Bussiness.Implementation
{
    public class ApplicationBussiness : IApplicationBussiness
    {
        public IApplicationDataAccess _dataAccess { get; set; }

        public ApplicationBussiness(IApplicationDataAccess dataAccess)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException("dataAccess");
            }

            _dataAccess = dataAccess;
        }

        public async Task<Application> Register(string name, string URI)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }

            if (string.IsNullOrWhiteSpace(URI))
            {
                throw new ArgumentException("URI");
            }

            return await _dataAccess.Register(name, URI);
        }

        public async Task<bool> Exists(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("id");
            }
            Guid guid = new Guid();

            if(!Guid.TryParse(id,out guid)){
                throw new InvalidCastException();
            }

            return await _dataAccess.Exists(guid);
        }
    }
}

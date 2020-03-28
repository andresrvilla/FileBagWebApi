using FileBagWebApi.DataAccess.Context;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Exceptions;
using FileBagWebApi.Entities.Models;
using FileBagWebApi.Utilities.NetCore.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileBagWebApi.DataAccess.EntityFramework
{
    public class ApplicationDataAccess : IApplicationDataAccess
    {
        private FileBagContext _context;

        private IInMemoryCache _memoryCache;

        private const string APPLICATION_KEY = "Application_{0}";
        

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ApplicationDataAccess(FileBagContext context, IInMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Application> Register(string name, string URI)
        {
            try
            {
                Application application = new Application()
                {
                    Name = name,
                    URI = URI
                };
                var result = await _context.Applications.AddAsync(application);

                if (result.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    var saveResult = await _context.SaveChangesAsync();
                    if (saveResult > 0)
                    {
                        return result.Entity;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
                throw new FileBagWebApiDatabaseException();
            }
            
        }

        public async Task<bool> Exists(Guid id)
        {
            try
            {
                bool result = false;
                string currentKey = string.Format(APPLICATION_KEY, id);
                if (_memoryCache.TryGetValue(currentKey,out result))
                {
                    return result;
                }
                else
                {
                    var app = await _context.Applications.FindAsync(id);
                    result = app != null;
                    _memoryCache.Set<bool>(currentKey, result,InMemoryCacheOffset.FiveMinutes);
                }
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new FileBagWebApiDatabaseException();
            }
        }
    }
}

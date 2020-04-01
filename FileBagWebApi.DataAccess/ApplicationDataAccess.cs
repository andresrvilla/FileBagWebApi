using CSharpUtilities.Entities;
using EnumUtilities;
using FileBagWebApi.DataAccess.Context;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Exceptions;
using FileBagWebApi.Entities.Models;
using FileBagWebApi.Utilities.NetCore.Interfaces;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Application> Register(string name, string secret, string URI)
        {
            try
            {
                Application application = new Application()
                {
                    Name = name,
                    URI = URI,
                    Secret = secret,
                    RowStatus = EnumUtil.ToByte(RowStatus.Active)
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
                throw new FileBagWebApiDatabaseException(ex.Message);
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
                    var app = await GetById(id);
                    result = app != null;
                    _memoryCache.Set<bool>(currentKey, result,InMemoryCacheOffset.FiveMinutes);
                }
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new FileBagWebApiDatabaseException(ex.Message);
            }
        }

        public async Task<Application> GetByIdAndSecret(Guid id, string secret)
        {
            try
            {
                byte status = EnumUtil.ToByte(RowStatus.Active);

                var query = await (from a in _context.Applications 
                                    where a.Id == id && 
                                    a.Secret == secret &&
                                    a.RowStatus == status
                                    select a).ToListAsync();

                return query.First();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new FileBagWebApiDatabaseException(ex.Message);
            }
        }

        public async Task<Application> GetById(Guid id)
        {
            try
            {
                byte status = EnumUtil.ToByte(RowStatus.Active);

                var query = await(from a in _context.Applications
                                  where a.Id == id &&
                                  a.RowStatus == status
                                  select a).ToListAsync();

                return query.First();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new FileBagWebApiDatabaseException(ex.Message);
            }
        }

        public async Task UpdateToken(Guid id, string token)
        {
            try
            {
                var app = await GetById(id);
                app.Token = token;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new FileBagWebApiDatabaseException(ex.Message);
            }
        }

        public Task RemoveToken(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}

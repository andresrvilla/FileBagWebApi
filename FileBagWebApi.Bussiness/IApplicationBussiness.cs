using FileBagWebApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Bussiness
{
    public interface IApplicationBussiness
    {
        public Application Register(string name, string URI);
    }
}

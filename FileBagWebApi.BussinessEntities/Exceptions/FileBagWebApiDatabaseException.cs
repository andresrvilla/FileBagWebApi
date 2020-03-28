using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Entities.Exceptions
{
    public class FileBagWebApiDatabaseException: Exception
    {
        public FileBagWebApiDatabaseException(): base("An error ocurred on data access")
        {

        }

        public FileBagWebApiDatabaseException(string message):base (message)
        {

        }
    }
}

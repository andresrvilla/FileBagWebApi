using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileBagWebApi.Entities.ViewModels
{
    public class TransactionResult
    {
        public bool ok { get; set; }

        public string errors { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Entities.ViewModels
{
    public class TokenRequestDTO
    {
        public string id { get; set; }

        public string secret { get; set; }
    }
}

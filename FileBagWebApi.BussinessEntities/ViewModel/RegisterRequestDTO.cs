using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Entities.ViewModels
{
    public class RegisterRequestDTO
    {
        public string name { get; set; }

        public string secret { get; set; }

        public string URI { get; set; }
    }
}

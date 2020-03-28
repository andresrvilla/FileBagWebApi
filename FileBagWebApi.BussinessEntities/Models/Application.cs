using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Entities.Models
{
    public class Application
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string URI { get; set; }
    }
}

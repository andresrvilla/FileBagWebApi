using CSharpUtilities.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Entities.Models
{
    public class Application: IRowStatus
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string URI { get; set; }

        public string Secret { get; set; }

        public string Token { get; set; }

        public byte RowStatus { get; set; }
    }
}

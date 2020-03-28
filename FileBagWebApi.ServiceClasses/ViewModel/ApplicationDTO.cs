using FileBagWebApi.Entities.Models;
using FileBagWebApi.ServiceClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.ServiceClasses.ViewModel
{
    public class ApplicationDTO: IDTO<Application, ApplicationDTO>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string URI { get; set; }

        public ApplicationDTO Build(Application be)
        {
            Id = be.Id.ToString();
            Name = be.Name;
            URI = be.URI;
            return this;
        }

        public Application GetBE()
        {
            return new Application()
            {
                Id = new Guid(Id),
                Name = Name,
                URI = URI
            };
        }
    }
}

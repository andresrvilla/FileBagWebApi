using System;

namespace FileBagWebApi.Models
{
    public class RequestIdentifier
    {
        public Guid ApplicationId { get; set; }

        public Guid EntityId { get; set; }
    }
}
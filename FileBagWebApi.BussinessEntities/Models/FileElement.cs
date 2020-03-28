using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Entities.Models
{
    public class FileElement: IAuditData
    {
        public Guid Id { get; set; }

        public Application Application { get; set; }

        public Guid ApplicationId { get; set; }

        public string EntityType { get; set; }

        public long ContentLength { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }

        public FileDetail FileDetail { get; set; }

        public string Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public string Modifier { get; set; }

        public DateTime ModificationDate { get; set; }

        public long AccessCount { get; set; }

        public FileStatus Status { get; set; }

    }
}

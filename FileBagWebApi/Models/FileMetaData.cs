using System;

namespace FileBagWebApi.Models
{
    public class FileMetaData
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid EntityTypeId { get; set; }

        public long ContentLength { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }

        public FileData Data { get; set; }

        public AuditData Audit { get; set; }
    }
}
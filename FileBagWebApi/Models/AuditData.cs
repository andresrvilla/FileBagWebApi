using System;

namespace FileBagWebApi.Models
{
    public class AuditData
    {
        public string Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public long AccessCount { get; set; }

        public FileStatus Status { get; set; }
    }
}
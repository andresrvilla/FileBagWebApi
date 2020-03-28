using System;

namespace FileBagWebApi.Entities.Models
{
    public interface IAuditData
    {
        string Creator { get; set; }

        DateTime CreationDate { get; set; }

        string Modifier { get; set; }

        DateTime ModificationDate { get; set; }

        long AccessCount { get; set; }
    }
}
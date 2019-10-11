using System;

namespace FileBagWebApi.Models
{
    public class FileData
    {
         public Guid Id { get; set; }

         public byte[] Contents { get; set; }
    }
}
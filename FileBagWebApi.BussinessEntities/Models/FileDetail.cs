using System;

namespace FileBagWebApi.Entities.Models
{
    public class FileDetail
    {
         public Guid Id { get; set; }

         public byte[] Contents { get; set; }
    }
}
using System;
using System.IO;

namespace FileBagWebApi.ViewModel
{
    public class FileDTO
    {
        public Guid id { get; set; }

        public long contentLength { get; set; }

        public string name { get; set; }

        public string mimeType { get; set; }

        public byte[] data { get; set; }
    }
}
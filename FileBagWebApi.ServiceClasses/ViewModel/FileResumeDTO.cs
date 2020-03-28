using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileBagWebApi.ViewModel
{
    public class FileResumeDTO
    {
        public Guid id { get; set; }

        public long contentLength { get; set; }

        public string name { get; set; }

        public string mimeType { get; set; }
    }
}

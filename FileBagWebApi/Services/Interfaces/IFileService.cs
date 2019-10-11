using System.Threading.Tasks;
using FileBagWebApi.Models;

namespace FileBagWebApi.Services.Interfaces
{
    public interface IFileService
    {
         Task<FileMetaData[]> AllActive(RequestIdentifier requestIdentifier);
    }
}
using FileBagWebApi.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileBagWebApi.Bussiness.Interfaces
{
    public interface IFileBussiness
    {
        Task<IList<FileResumeDTO>> AllActive(RequestIdentifier requestIdentifier);

        Task<FileDTO> FileById(RequestIdentifier requestIdentifier, Guid id);

        Task<FileResumeDTO> FileResumeById(RequestIdentifier requestIdentifier, Guid id);

        Task<TransactionResult> RemoveFile(RequestIdentifier requestIdentifier, Guid id);

        Task<FileResumeDTO> AddFile(RequestIdentifier request, FileDTO fileDTO);

        //Task<FileResumeDTO> StartAddFile(RequestIdentifier request);

        //Task<FileResumeDTO> EndAddFile(RequestIdentifier requestIdentifier, FileDTO fileDTO);

        //Task<FileElementResume> UpdateMetadata(FileRequestDTO request, FileDTO fileDTO);
    }
}
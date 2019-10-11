using System;
using System.Threading.Tasks;
using FileBagWebApi.Models;
using FileBagWebApi.Services.Interfaces;

namespace FileBagWebApi.Services
{
    public class FakeFileService : IFileService
    {
        public Task<FileMetaData[]> AllActive(RequestIdentifier requestIdentifier)
        {
            FileMetaData file1 = new FileMetaData(){
                Id=Guid.NewGuid(),
                Name="Fake1.docx",
                MimeType="application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ContentLength=100,
                Data=new FileData(){
                    Contents=new byte[100],
                    Id=Guid.NewGuid()
                },
                Audit= new AuditData(){
                    AccessCount=0,
                    CreationDate=DateTime.Now,
                    Creator="SYSTEM",
                    Status=FileStatus.Approved
                }
            };

            new Random().NextBytes(file1.Data.Contents);

            FileMetaData file2 = new FileMetaData(){
                Id=Guid.NewGuid(),
                Name="Fake2.docx",
                MimeType="application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ContentLength=200,
                Data=new FileData(){
                    Contents=new byte[200],
                    Id=Guid.NewGuid()
                },
                Audit= new AuditData(){
                    AccessCount=0,
                    CreationDate=DateTime.Now,
                    Creator="USER",
                    Status=FileStatus.Approved
                }
            };

            new Random().NextBytes(file2.Data.Contents);

            return Task.FromResult(new[] {file1, file2});
        }
    }
}
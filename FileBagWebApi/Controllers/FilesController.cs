using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileBagWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FilesController : ControllerBase
    {
        private IFileBussiness fileService;

        public FilesController(IFileBussiness fileService) => this.fileService = fileService;

        /// <summary>
        /// Returns all active Files Resume
        /// </summary>
        /// <param name="identifier">Request Identifier</param>
        /// <returns>A list of all active resumes</returns>
        [HttpGet("{applicationId}/{entityTypeId}")]
        public async Task<ActionResult<IEnumerable<FileResumeDTO>>> All(string applicationId, string entityTypeId)
        {
            var identifier = new RequestIdentifier() { applicationId = applicationId, entityTypeId = entityTypeId };
            return Ok(await fileService.AllActive(identifier));
        }

        /// <summary>
        /// Returns the FileResume identified by <see cref="id"/>
        /// </summary>
        /// <param name="identifier">Request Identifier</param>
        /// <param name="id">File Identifier</param>
        /// <returns>The resume identified by <see cref="id"/></returns>
        [HttpGet("{applicationId}/{entityTypeId}/{id}/resume")]
        public async Task<ActionResult<FileResumeDTO>> GetResume(string applicationId, string entityTypeId, string id)
        {
            var identifier = new RequestIdentifier() { applicationId = applicationId, entityTypeId = entityTypeId };
            return Ok(await fileService.FileResumeById(identifier, new Guid(id)));
        }

        /// <summary>
        /// Returns the FileResume identified by <see cref="id"/>
        /// </summary>
        /// <param name="identifier">Request Identifier</param>
        /// <param name="id">File Identifier</param>
        /// <returns>The resume identified by <see cref="id"/></returns>
        [HttpGet("{applicationId}/{entityTypeId}/{id}")]
        public async Task<ActionResult<FileResumeDTO>> Get(string applicationId, string entityTypeId, string id)
        {
            var identifier = new RequestIdentifier() { applicationId = applicationId, entityTypeId = entityTypeId };
            return Ok(await fileService.FileById(identifier, new Guid(id)));
        }
        /*
        // POST api/values
        [HttpPost]
        public async Task<ActionResult<IEnumerable<FileResumeDTO>>> Post(RequestIdentifier identifier)
        {
            List<FileElementResume> result = new List<FileElementResume>();
            foreach (var file in Request.Form.Files)
            {
                FileDTO fileDTO = new FileDTO()
                {
                    contentLength = file.Length,
                    data = null,
                    mimeType = file.ContentType,
                    name = file.Name
                };

                await file.CopyToAsync(fileDTO.data);

                result.Add(await fileService.AddFile(identifier, fileDTO));
            }
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult<FileElementResume>> Put(FileRequestDTO fileDetailDTO)
        {
            List<FileElementResume> result = new List<FileElementResume>();
            foreach (var file in Request.Form.Files)
            {

                FileDTO fileDTO = new FileDTO()
                {
                    contentLength = file.Length,
                    data = null,
                    mimeType = file.ContentType,
                    name = file.Name
                };

                await file.CopyToAsync(fileDTO.data);

                result.Add(await fileService.AddOrUpdateFile(fileDetailDTO, fileDTO));
            }
            return Ok(result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionResult>> Delete(RequestIdentifier identifier, string id)
        {
            return Ok(await fileService.RemoveFile(identifier, new Guid(id)));
        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileBagWebApi.Models;
using FileBagWebApi.Services;
using FileBagWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileBagWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFileService fileService;

        public FilesController(IFileService fileService) => this.fileService = fileService;

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileMetaData>>> Get(string ApplicationId,string EntityTypeId)
        {
            RequestIdentifier identifier = new RequestIdentifier(){
                ApplicationId=new Guid(ApplicationId),
                EntityTypeId=new Guid(EntityTypeId)
            };
            return Ok(await fileService.AllActive(identifier));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

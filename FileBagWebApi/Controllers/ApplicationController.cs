using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.ServiceClasses.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileBagWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public IApplicationBussiness _applicationBussiness { get; set; }

        public ApplicationController(IApplicationBussiness applicationBussiness)
        {
            _applicationBussiness = applicationBussiness;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDTO registerDTO)
        {
            try
            {
                var result = await _applicationBussiness.Register(registerDTO.name, registerDTO.URI);
                if (result != null)
                {
                    return Ok(new ApplicationDTO().Build(result));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/exists")]
        public async Task<IActionResult> Exists(string id)
        {
            try
            {
                var result = await _applicationBussiness.Exists(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
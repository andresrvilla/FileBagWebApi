using CSharpUtilities.Net.Rest.DTOClasses;
using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.Entities.ViewModels;
using FileBagWebApi.Infraestructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ApplicationController : ControllerBase
    {
        private readonly IServiceTokenValidator _serviceTokenValidator;

        private readonly IApplicationBussiness _applicationBussiness;


        public ApplicationController(IApplicationBussiness applicationBussiness,IServiceTokenValidator serviceTokenValidator)
        {
            _applicationBussiness = applicationBussiness;
            _serviceTokenValidator = serviceTokenValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDTO registerDTO)
        {
            try
            {
                var result = await _applicationBussiness.Register(registerDTO.name, registerDTO.secret, registerDTO.URI);
                if (result != null)
                {
                    var app = new ApplicationDTO().Build(result);
                    return Ok(new RestResponseObjectDTO(app));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new RestErrorResponseDTO(StatusCodes.Status500InternalServerError));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RestErrorResponseDTO(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> Token([FromBody]TokenRequestDTO tokenRequestDTO)
        {
            try
            {
                var result = await _applicationBussiness.GetToken(tokenRequestDTO.id, tokenRequestDTO.secret);
                if (result != null)
                {
                    //var app = new ApplicationDTO().Build(result);
                    return Ok(new RestResponseObjectDTO(result));
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new RestErrorResponseDTO(StatusCodes.Status404NotFound));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RestErrorResponseDTO(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("WhoIAm")]
        public async Task<IActionResult> WhoIAm()
        {
            try
            {
                var validation = _serviceTokenValidator.Validate(Request.Headers);
                if (validation != null)
                {
                    return validation;
                }

                var result = await _applicationBussiness.GetByToken(Request.Headers.GetToken());
                if (result != null)
                {
                    var app = new ApplicationDTO().Build(result);
                    return Ok(new RestResponseObjectDTO(app));
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new RestErrorResponseDTO(StatusCodes.Status404NotFound));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RestErrorResponseDTO(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
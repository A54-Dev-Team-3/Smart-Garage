using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Services.Contracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/parts")]
    [Authorize]
    public class PartApiController : ControllerBase
    {
        private readonly IPartService partService;

        public PartApiController(IPartService partService)
        {
            this.partService = partService;
        }

        [HttpPost("")] // api/parts/
        public async Task<ActionResult<Part>> Create([FromBody] PartRequestDTO newPartDTO)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var newPart = partService.Create(newPartDTO, username);
                return Ok(newPart);
            }
            catch (DuplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // GetAll: Get all Parts or filter by parameters
        [HttpGet("")] // api/parts/
        public IActionResult GetAll([FromQuery] PartQueryParameters filterParameters)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var parts = partService.FilterBy(filterParameters, username);
                return Ok(parts);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet("{id}")] // api/parts/{id}
        public IActionResult GetById(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var parts = partService.GetById(int.Parse(id), username);
                return Ok(parts);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("{id}")] // api/parts/{id}
        public IActionResult Update(string id, [FromBody] PartRequestDTO newPartDTO)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var newPart = partService.Update(int.Parse(id), newPartDTO, username);
                return Ok(newPart);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete("{id}")] // api/service/{id}
        public IActionResult Delete(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                partService.Delete(int.Parse(id), username);
                return Ok($"Service with id:[{id}] deleted successfully.");
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}

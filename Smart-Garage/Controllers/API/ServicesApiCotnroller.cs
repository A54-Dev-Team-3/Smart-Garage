using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;
using Smart_Garage.Exceptions;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using System.Security.Claims;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/services")]
    [Authorize]
    public class ServicesApiCotnroller : ControllerBase
    {
        private readonly IServiceService servicesService;

        public ServicesApiCotnroller(IServiceService servicesService)
        {
            this.servicesService = servicesService;
        }

        [HttpPost("")] // api/services/
        public async Task<ActionResult<Service>> Create([FromBody] ServiceRequestDTO newServiceDTO)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var newService = servicesService.Create(newServiceDTO, username);
                return Ok(newService);
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

        // GetAll: Get all Services or filter by parameters
        [HttpGet("")] // api/services/
        public IActionResult GetAll([FromQuery] ServicesQueryParameters filterParameters)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var services = servicesService.FilterBy(filterParameters, username);
                return Ok(services);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }

        [HttpGet("{id}")] // api/services/{id}
        public IActionResult GetById(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var service = servicesService.GetById(int.Parse(id), username);
                return Ok(service);
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

        [HttpPut("{id}")] // api/services/{id}
        public IActionResult Update(string id, [FromBody] ServiceRequestDTO newService)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var updatedUser = servicesService.Update(int.Parse(id), newService, username);
                return Ok(updatedUser);
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
                servicesService.Delete(int.Parse(id), username);
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

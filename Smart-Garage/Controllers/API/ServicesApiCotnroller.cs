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
        private readonly IServicesService servicesService;

        public ServicesApiCotnroller(IServicesService servicesService)
        {
            this.servicesService = servicesService;
        }

        // GetAll: Get all Users or filter by parameters
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

        // GetById
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

        // Create
        [HttpPost("")] // api/services/
        public async Task<ActionResult<Service>> Create([FromBody] CreateServiceRequestDTO newServiceDTO)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var newUser = servicesService.Create(newServiceDTO, username);
                return Ok(newUser);
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

        // Update
        [HttpPut("{id}")] // api/services/{id}
        public IActionResult Update(string id, [FromBody] UpdateServiceRequestDTO newService)
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

        // Delete
        [HttpDelete("{id}")] // api/service/{id}
        public IActionResult Delete(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                servicesService.Delete(int.Parse(id), username);
                return Ok($"User with id:[{id}] deleted successfully.");
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

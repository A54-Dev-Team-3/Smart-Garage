using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;
using Smart_Garage.Exceptions;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

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
            // TODO
            var services = servicesService.FilterBy(filterParameters);
            return Ok(services);
        }

        // GetById
        [HttpGet("{id}")] // api/services/{id}
        public IActionResult GetById(string id)
        {
            try
            {
                var service = servicesService.GetById(int.Parse(id));
                return Ok(service);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Create
        [HttpPost("")] // api/services/create
        public async Task<ActionResult<Service>> Create([FromBody] CreateServiceRequestDTO newServiceDTO)
        {
            try
            {
                var newUser = servicesService.Create(newServiceDTO);
                return Ok(newUser);
            }
            catch (DuplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update
        [HttpPut("{id}")] // api/services/{id}
        public IActionResult Update(string id, [FromBody] UpdateServiceRequestDTO newService)
        {
            try
            {
                var updatedUser = servicesService.Update(int.Parse(id), newService, "");
                return Ok(updatedUser);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Delete
        [HttpDelete("{id}")] // api/service/{id}
        public IActionResult Delete(string id)
        {
            try
            {
                servicesService.Delete(int.Parse(id));
                return Ok($"User with id:[{id}] deleted successfully.");
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

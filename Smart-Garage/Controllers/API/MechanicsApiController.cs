using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;
using System.Security.Claims;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/mechanics")]
    [Authorize]
    public class MechanicsApiController : ControllerBase
    {
        private readonly IMechanicService mechanicService;
        public MechanicsApiController(IMechanicService mechanicService)
        {
            this.mechanicService = mechanicService;
        }

        [HttpPost("")] // api/mechanics/
        public async Task<ActionResult<Mechanic>> Create([FromBody] MechanicRequestDTO newMechanic)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var result = mechanicService.Create(newMechanic, username);
                return Ok(result);
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

        // GetAll: Get all Visits or filter by parameters
        [HttpGet("")] // api/mechanics/
        public IActionResult GetAll()
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var services = mechanicService.GetAll(username);
                return Ok(services);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet("{id}")] // api/mechanics/{id}
        public IActionResult GetById(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var service = mechanicService.GetById(int.Parse(id), username);
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

        [HttpDelete("{id}")] // api/mechanics/{id}
        public IActionResult Delete(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                mechanicService.Delete(int.Parse(id), username);
                return Ok($"Mechanic with id:[{id}] deleted successfully.");
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;
using System.Security.Claims;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/visits")]
    [Authorize]
    public class VisitsApiController : ControllerBase
    {
        private readonly IVisitService visitsService;

        public VisitsApiController(IVisitService visitsService)
        {
            this.visitsService = visitsService;
        }

        [HttpPost("")] // api/services/
        public async Task<ActionResult<Visit>> Create([FromHeader] int VehicleId)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var newVisit = visitsService.Create(VehicleId, username);
                return Ok(newVisit);
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
        [HttpGet("")] // api/visits/
        public IActionResult GetAll([FromQuery] VisitQueryParameters filterParameters)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var services = visitsService.FilterBy(filterParameters, username);
                return Ok(services);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet("{id}")] // api/visits/{id}
        public IActionResult GetById(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var service = visitsService.GetById(int.Parse(id));
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

        [HttpDelete("{id}")] // api/visits/{id}
        public IActionResult Delete(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                visitsService.Delete(int.Parse(id), username);
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

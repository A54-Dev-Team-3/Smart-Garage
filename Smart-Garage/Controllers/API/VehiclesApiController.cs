using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Repositories.Contracts;
using System.Security.Cryptography.Xml;
using Smart_Garage.Services.Contracts;
using Smart_Garage.Models.DTOs.RequestDTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Smart_Garage.Helpers.Contracts;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using AutoMapper;
using System.Runtime.InteropServices;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.QueryParameters;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/vehicles")]
    [Authorize]
    public class VehiclesApiController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly IUserService usersService;
        private readonly IModelMapper modelMapper;
        private readonly IMapper autoMapper;
        public VehiclesApiController(IVehicleService vehicleService, IUserService usersService, IModelMapper modelMapper, IMapper autoMapper)
        {
            this.vehicleService = vehicleService;
            this.usersService = usersService;
            this.modelMapper = modelMapper;
            this.autoMapper = autoMapper;
        }

        //api/vehicles
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehicleRequestDTO dto)
        {
            try
            {
                var user = GetUser();

                var createdVehicle = vehicleService.Create(user.Username, dto);

                return StatusCode(StatusCodes.Status201Created, createdVehicle);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // GetAll: Get all Users or filter by parameters
        //api/vehicles
        [HttpGet]
        public IActionResult GetAllVehicles([FromQuery] VehicleQueryParameters filter)
        {
            try
            {
                var user = GetUser();

                var vehicles = this.vehicleService.FilterBy(filter);

                return Ok(vehicles);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //api/vehicles/id
        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            try
            {
                var vehicle = vehicleService.GetById(id);

                return Ok(vehicle);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //api/vehicles/id
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] VehicleRequestDTO dto)
        {
            try
            {
                var user = GetUser();

                var updatedVehicle = vehicleService.Update(id, dto);

                return Ok(updatedVehicle);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }



        //api/vehicles/id
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            try
            {
                var user = GetUser();

                vehicleService.Delete(id);

                return NoContent();
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        private UserRequestDTO GetUser()
        {
            var user = usersService.GetByName(User.FindFirst(ClaimTypes.Name)?.Value);
            if(!user.IsAdmin)
            {
                throw new UnauthorizedOperationException("You are not an admin!");
            }
            return autoMapper.Map<UserRequestDTO>(user);
        }
    }
}

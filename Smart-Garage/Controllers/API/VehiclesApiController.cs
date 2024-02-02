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

                var vehicle = modelMapper.Map(dto, autoMapper.Map<User>(user));

                var createdVehicle = vehicleService.Create(user, vehicle);
                VehicleResponseDTO responseDto = autoMapper.Map<VehicleResponseDTO>(createdVehicle);

                return StatusCode(StatusCodes.Status201Created, responseDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //api/vehicles/id
        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            try
            {
                var createdVehicle = vehicleService.GetById(id);

                VehicleResponseDTO responseDto = autoMapper.Map<VehicleResponseDTO>(createdVehicle);

                return Ok(responseDto);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //api/vehicles/id
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] VehicleRequestDTO dto)
        {
            try
            {
                var user = GetUser();

                var vehicle = modelMapper.Map(dto, autoMapper.Map<User>(user));

                var updatedVehicle = vehicleService.Update(id, vehicle);
                VehicleResponseDTO responseDTO = autoMapper.Map<VehicleResponseDTO>(updatedVehicle);

                return Ok(responseDTO);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //api/vehicles/id
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            try
            {
                vehicleService.Delete(id);

                return NoContent();
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET: api/vehicles?filterParameter=filter
        [HttpGet]
        public IActionResult FilterVehicles([FromQuery] VehicleQueryParameters filters)
        {
            var user = GetUser();

            List<VehicleResponseDTO> vehicles = vehicleService
                .FilterBy(filters)
                .Select(vehicle => modelMapper.Map(user, vehicle))
                .ToList();

            if(vehicles.Count > 0)
            {
                return Ok(vehicles);
            }
            return NoContent();
        }

        private UserRequestDTO GetUser()
        {
            
            var user = usersService.GetByName(User.FindFirst(ClaimTypes.Name)?.Value);
            return autoMapper.Map<UserRequestDTO>(user);
        }
    }
}

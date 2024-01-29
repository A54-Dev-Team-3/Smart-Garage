using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Helpers;
using Smart_Garage.Helpers.Contracts;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Services.Contracts;
using System.Net;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersApiController : ControllerBase
    {
        private readonly IUsersService userService;
        private readonly IConfiguration configuration;

        public UsersApiController(IUsersService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        // GetAll: Get all Users or filter by parameters
        [HttpGet("")] // api/users/
        public IActionResult GetUsers([FromQuery] UserQueryParameters filterParameters)
        {
            // TODO
            IList<User> users = userService.FilterBy(filterParameters);
            return Ok(users);
        }

        // GetById
        [HttpGet("{id}")] // api/users/{id}
        public IActionResult GetById(string id)
        {
            try
            {
                var user = userService.GetById(int.Parse(id));
                return Ok(user);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Create
        [HttpPost("signup")] // api/users/signup
        [AllowAnonymous]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpUserDTO userRequestDTO)
        {
            try
            {
                UserResponseDTO newUser = userService.Create(userRequestDTO);
                return Ok(newUser);
            }
            catch (DuplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Login
        [HttpPost("login")] // api/users/login
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserRequestDTO requestDTO)
        {
            try
            {
                string token = userService.Login(requestDTO);
                return Ok(token);
            }
            catch (WrongPasswordException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update
        [HttpPut("{id}")] // api/users/{id}
        public IActionResult Update(string id, [FromBody] UpdateUserDTO updatedUser)
        {
            try
            {
                UserResponseDTO result = userService.Update(int.Parse(id), updatedUser);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Delete
        [HttpDelete("{id}")] // api/users/{id}
        public IActionResult DeleteUser(string id, [FromHeader] string username)
        {
            try
            {
                userService.Delete(int.Parse(id), username);
                return Ok($"User with id:[{id}] deleted successfully.");
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Services.Contracts;
using System.Security.Claims;
using System.Security.Principal;

namespace Smart_Garage.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public UsersApiController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        // Create
        [HttpPost("signup")] // api/users/signup
        [AllowAnonymous]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpUserRequestDTO userRequestDTO)
        {
            try
            {
                var newUser = userService.Create(userRequestDTO);
                return Ok(newUser);
            }
            catch (DuplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GetAll: Get all Users or filter by parameters
        [HttpGet("")] // api/users/
        public IActionResult GetAll([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var users = userService.FilterBy(filterParameters);
                return Ok(users);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
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

        // Login
        [HttpPost("login")] // api/users/login
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserRequestDTO requestDTO)
        {
            try
            {
                var token = userService.Login(requestDTO);
                return Ok(token);
            }
            catch (WrongPasswordException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Update
        [HttpPut("{id}")] // api/users/{id}
        public IActionResult Update(string id, [FromBody] UpdateUserRequestDTO updatedUser)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                var result = userService.Update(int.Parse(id), updatedUser, username);
                    
                // TODO: [Question] How to update Claim Type

                return Ok(result);
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
        [HttpDelete("{id}")] // api/users/{id}
        public IActionResult Delete(string id)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                userService.Delete(int.Parse(id), username);
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

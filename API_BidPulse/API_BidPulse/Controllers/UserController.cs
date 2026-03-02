using API_BidPulse.Core.Interfaces;
using API_BidPulse.Data.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_BidPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service;
        }




        [HttpGet]
        [SwaggerOperation(
         Summary = "Get all users",
        Description = "Returns all registered users without passwords."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            var result = users.Select(u => new
            {
                u.UserId,
                u.Name,
                u.Email
            });
            return Ok(result);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(
         Summary = "Get user by id",
        Description = "Returns a specific user without password information."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(new
            {
                user.UserId,
                user.Name,
                user.Email
            });
        }


        [HttpPost("register")]
        [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Creates a new user account with name, email and password. The email must be unique."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Name, Email and Password need to be filled in.");
            }

            var ok = await _userService.RegisterAsync(dto);
            if (!ok) return BadRequest("Email does already exists or the data in invalid");
            return Ok(new
            {
                message ="User registered successfully!"
            });
        }


        [HttpPost("login")]
        [SwaggerOperation(
        Summary = "Login user",
        Description = "Validates user email and password and returns the user id if successful."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (dto == null ||
              string.IsNullOrWhiteSpace(dto.Email) ||
              string.IsNullOrWhiteSpace(dto.Password))
                return Unauthorized("Wrong email or password!");

            var user = await _userService.LoginAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Wrong email or password!");

            return Ok(new
            {
                userId = user.UserId,
                name = user.Name,
                email = user.Email
            });
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = "Update a user",
         Description = "Updates user information. Only provided fields will be updated."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UserUpdateDTO dto)
        {
            var ok = await _userService.UpdateUserAsync(id, dto);
            if (!ok) return NotFound();

            return Ok(new
            {
                message="User updated succesfully!"
            });
        }

        // DELETE USER
        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Delete a user",
         Description = "Deletes a user by id."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _userService.DeleteUserAsync(id);
            if(!ok) return NotFound();
            
            return NoContent();
        }
    }
}

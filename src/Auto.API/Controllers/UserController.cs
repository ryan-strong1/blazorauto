using Auto.API.Models;
using Auto.BizLogic;
using Auto.BizLogic.Models.Dto;
using Auto.BizLogic.Services;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Auto.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAutoService _autoService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService, IAutoService autoService)
        {
            _userService = userService;
            _autoService = autoService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetUsers()
        {
            var users = await _userService.GetUsers();

            return Ok(new ApiResponse<IEnumerable<UserDto>>(users, message: "Users retrieved successfully"));
        }

        [HttpGet("{id}/autos")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AutoDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AutoDto>>>> GetUserAutos(int id)
        {
            var autos = await _autoService.GetAutosByUserId(id);

            return Ok(new ApiResponse<IEnumerable<AutoDto>>(autos, message: "Autos retrieved successfully"));
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult<ApiResponse<UserEntity>>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserbyEmail(email);

            if (user == null)
            {
                return NotFound(new ApiResponse<string>("User not found", success: false));
            }

            return Ok(new ApiResponse<UserDto>(user, message: "User retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById(int id)
        {
            var user = await _userService.GetUserbyId(id);

            if (user == null)
            {
                return NotFound(new ApiResponse<string>("User not found", success: false));
            }

            return Ok(new ApiResponse<UserDto>(user, message: "User retrieved successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<ApiResponse<string>>> DeleteUserById(int id)
        {
            var result = await _userService.DeleteUserByIdAsync(id);

            if (result == false)
            {
                return NotFound(new ApiResponse<string>("User not found", success: false));
            }

            return NoContent();
        }
    }
}
using GameStore.Dtos.User;
using GameStore.Interfaces;
using GameStore.Interfaces.IServices;
using GameStore.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        if (registerUserDto == null)
            return BadRequest("User data is required");

        bool isRegistered = await _userService.RegisterAsync(registerUserDto);

        if (!isRegistered)
            return BadRequest("User registration failed");

        return CreatedAtAction(nameof(Register), new { email = registerUserDto.Email }, registerUserDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        if (string.IsNullOrEmpty(loginUserDto.Email) || string.IsNullOrEmpty(loginUserDto.Password))
            return BadRequest("Email or password is required!"); ;

        bool isLogin = await _userService.LoginAsync(loginUserDto.Email, loginUserDto.Password);

        if (!isLogin)
            return BadRequest("Loging failed!");

        return Ok(isLogin);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = _userService.GetByIdAsync(id);
        return Ok(user);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
    {
        if (updateUserDto == null)
            return BadRequest("User data is required");

        var updateUser = await _userService.UpdateProfileAsync(id, updateUserDto);

        if (!updateUser) 
            return NotFound("User not found");

        return Ok("User updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool delUser = await _userService.DeleteAccountAsync(id);

        if (!delUser)
            return BadRequest("User removal failed");

        return Ok("User removed successfully");
    }
}

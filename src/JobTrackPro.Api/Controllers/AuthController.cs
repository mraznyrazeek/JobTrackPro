using JobTrackPro.Application.DTOs.Auth;
using JobTrackPro.Application.Interfaces;
using JobTrackPro.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest(new AuthResponseDto
            {
                IsSuccess = false,
                Message = "User already exists with this email."
            });
        }

        var user = new ApplicationUser
        {
            FullName = request.FullName,
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new AuthResponseDto
            {
                IsSuccess = false,
                Message = string.Join(", ", result.Errors.Select(x => x.Description))
            });
        }

        return Ok(new AuthResponseDto
        {
            IsSuccess = true,
            Message = "User registered successfully.",
            Email = user.Email,
            FullName = user.FullName
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Unauthorized(new AuthResponseDto
            {
                IsSuccess = false,
                Message = "Invalid email or password."
            });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new AuthResponseDto
            {
                IsSuccess = false,
                Message = "Invalid email or password."
            });
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user, roles);

        return Ok(new AuthResponseDto
        {
            IsSuccess = true,
            Message = "Login successful.",
            Token = token,
            Email = user.Email,
            FullName = user.FullName
        });
    }
}
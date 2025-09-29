using Lab5estebanpacheco.DTOs;
using Lab5estebanpacheco.Models;
using Lab5estebanpacheco.Repositories.Interfaces;
using Lab5estebanpacheco.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab5estebanpacheco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _jwtKey = "MiClaveSuperSecretaDe32CaracteresMinimo123456bua12"; // Igual que en Program.cs

    public AuthController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var exists = (await _unitOfWork.Repository<user>().FindAsync(u => u.username == userDto.username)).Any();
        if (exists) return BadRequest("El usuario ya existe.");

        var usuario = new user
        {
            username = userDto.username,
            password_hash = PasswordHelper.HashPassword(userDto.password),
            role = "User"
        };

        await _unitOfWork.Repository<user>().AddAsync(usuario);
        await _unitOfWork.SaveAsync();

        return Ok("Usuario registrado correctamente.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var usuario = (await _unitOfWork.Repository<user>().FindAsync(u => u.username == userDto.username)).FirstOrDefault();
        if (usuario == null || usuario.password_hash != PasswordHelper.HashPassword(userDto.password))
            return Unauthorized("Usuario o contraseña incorrectos.");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.username),
            new Claim(ClaimTypes.Role, usuario.role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult SoloAdmin()
    {
        return Ok("Solo para administradores");
    }

    [HttpGet("user")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult SoloUsuarios()
    {
        return Ok("Para usuarios y administradores");
    }
}

using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.Linq;
using System;
using JWTTokenAuthenticationPOC.Models;
using JWTTokenAuthenticationPOC.Data;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(ApplicationDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public IActionResult Register() => View();

    [HttpPost("Auth/Register")]
    public IActionResult Register(User user)
    {
        if (_context.Users.Any(u => u.Username == user.Username))
        {
            ViewBag.Message = "Username already exists.";
            return View();
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        _context.SaveChanges();

        ViewBag.Message = "User registered successfully.";
        return View();
    }

    public IActionResult Login() => View();

    [HttpPost("Auth/Login")]
    public IActionResult Login(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

        if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash))
        {
            ViewBag.Message = "Invalid credentials.";
            return View();
        }

        var token = _jwtService.GenerateToken(existingUser.Id.ToString(), existingUser.Role);

        var userToken = new Token
        {
            UserId = existingUser.Id.ToString(),
            JwtToken = token,
            ExpiryDate = DateTime.UtcNow.AddMinutes(60)
        };

        _context.Tokens.Add(userToken);
        _context.SaveChanges();

        ViewBag.Message = "Login successful. Your token is stored in the database.";
        return View();
    }
}

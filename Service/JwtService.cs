﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;

    public JwtService(IConfiguration config)
    {
        _secretKey = config["JwtSettings:SecretKey"];
        _issuer = config["JwtSettings:Issuer"];
        _audience = config["JwtSettings:Audience"];
        _expiryMinutes = int.TryParse(config["JwtSettings:ExpiryMinutes"], out int minutes) ? minutes : 60;

        // Debug: Print values to console
        Console.WriteLine($"SecretKey: {_secretKey}");
        Console.WriteLine($"Issuer: {_issuer}");
        Console.WriteLine($"Audience: {_audience}");
        Console.WriteLine($"ExpiryMinutes: {_expiryMinutes}");

        if (string.IsNullOrEmpty(_secretKey) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
        {
            throw new ArgumentNullException("JwtSettings values cannot be null. Check appsettings.json!");
        }
    }

    public string GenerateToken(string userId, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

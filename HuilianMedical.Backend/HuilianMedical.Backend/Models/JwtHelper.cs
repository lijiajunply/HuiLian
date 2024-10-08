﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HuiLianMedical.Share.Data;
using Microsoft.IdentityModel.Tokens;

namespace HuilianMedical.Backend.Models;

public class JwtHelper(IConfiguration configuration)
{
    public string GetMemberToken(UserModel model)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim(ClaimTypes.Role, model.Identity),
            new Claim(ClaimTypes.NameIdentifier, model.Id)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));
        const string algorithm = SecurityAlgorithms.HmacSha256;
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now, //notBefore
            expires: DateTime.Now.AddSeconds(30), //expires
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
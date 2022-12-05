using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.API.DAL;
using Identity.API.DAL.Models;
using Identity.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IdentityDbContext _db;
    private List<string> _availableRoles = new() { "User","Admin"};

    public AccountController(IdentityDbContext db)
    {
        _db = db;
    }

    [HttpPost("sign-up", Name = "Sign Up")]
    public async Task<IActionResult> SignUp(SignUp request)
    {
        if (request is null) return BadRequest("Invalid client request");
        if (await _db.Users.AnyAsync(_ => _.Email == request.Email))
            return UnprocessableEntity("This email is already in use.");
        if (request.Password.Equals(request.PasswordConfirm) is false)
            return UnprocessableEntity("Passwords doesn't match.");
        if (_availableRoles.Contains(request.Role) is false)
            return BadRequest($"Internal error. Role '{request.Role}' not found.");
        _db.Users.Add(new User
        {
            Email = request.Email,
            Password = request.Password,
            Role = request.Role,
            FirstName = request.FirstName,
            LastName = request.LastName,
        });
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("sign-in",Name = "Sign In")]
    public async Task<IActionResult> SignIn(SignIn request)
    {
        if (request is null) return BadRequest("Invalid client request");
        
        var user = await _db.Users
            .FirstOrDefaultAsync(_ => _.Email == request.Email && _.Password == request.Password);
        if (user is default(User)) return Unauthorized("No user with given credentials found.");

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretPAIRjwtPrivateKey"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            "https://localhost:5011",
            "https://localhost:5011",
            new List<Claim>
            {
                new("id",$"{user.Id}"),
                new("firstName",$"{user.FirstName}"),
                new("lastName",$"{user.LastName}"),
                new("role",$"{user.Role}")
            },
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: signinCredentials
            );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return Ok(new JwtDto
        {
            Token = tokenString
        });
    }
}
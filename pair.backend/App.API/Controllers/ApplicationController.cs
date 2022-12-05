using System.Security.Claims;
using App.API.DAL;
using App.API.DAL.Models;
using App.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _accessor;

    public ApplicationController(ApplicationDbContext db, IHttpContextAccessor accessor)
    {
        _db = db;
        _accessor = accessor;
    }

    [Authorize]
    [HttpGet("{applicationId:guid}")]
    public async Task<IActionResult> GetApplication(Guid applicationId)
    {
        var result = await _db.Applications
            .Include(_ => _.Vehicle)
            .Include(_ => _.UserDto)
            .FirstOrDefaultAsync(_ => _.Id == applicationId);
        if (result is null) return NotFound();
        if (CanGetAccessToApplication(result.UserId) is false) 
            return Unauthorized("It's not your application");
        return Ok(result);
    }

    private bool CanGetAccessToApplication(Guid userId)
    {
        if (IsAdmin()) return true;
        return userId == GetUserId();
    }

    [Authorize]
    [HttpPost("add", Name = "Add Application")]
    public async Task<IActionResult> AddApplication(AddApplication request)
    {
        var user = _accessor.HttpContext.User;
        _db.Applications.Add(new Application
        {
            Status = Status.Waiting,
            Vehicle = request.Vehicle,
            DateAdded = DateTime.UtcNow,
            UserId = GetUserId(),
            FinalRegistrationNumber = request.DesiredRegistrationNumber,
            UserDto = GetUser()
        });
        await _db.SaveChangesAsync();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("accept", Name = "Accept Application")]
    public async Task<IActionResult> AcceptApplication(AcceptApplication request)
    {
        var application = await _db.Applications
            .FirstOrDefaultAsync(_ => _.Id == request.ApplicationId);
        if (application is null) return BadRequest("Invalid application id");
        application.Status = Status.Accepted;
        application.FinalRegistrationNumber = request.RegistrationNumber;
        application.DateFinished = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("reject", Name = "Reject Application")]
    public async Task<IActionResult> RejectApplication(RejectApplication request)
    {
        var application = await _db.Applications
            .FirstOrDefaultAsync(_ => _.Id == request.ApplicationId);
        if (application is null) return BadRequest("Invalid application id");
        application.Status = Status.Rejected;
        application.ReasonRejected = request.Reason;
        application.DateFinished = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [Authorize]
    [HttpGet(Name = "List User's Applications")]
    public async Task<IActionResult> ListUserApplications()
    {
        var user = _accessor.HttpContext.User;
        var applications = await _db.Applications
            .Where(_ => _.UserId == GetUserId())
            .Select(_ => new ApplicationListItem
            {
                Id = _.Id,
                Make = _.Vehicle.Make,
                Model = _.Vehicle.Model,
                Status = _.Status,
                DateAdded = _.DateAdded,
                DateFinished = _.DateFinished,
            })
            .ToListAsync();
        return Ok(applications);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all",Name = "List All Applications")]
    public async Task<IActionResult> ListAllApplications()
    {
        var applications = await _db.Applications
            .Select(_ => new ApplicationListItem
            {
                Id = _.Id,
                Make = _.Vehicle.Make,
                Model = _.Vehicle.Model,
                Status = _.Status,
                DateAdded = _.DateAdded,
                DateFinished = _.DateFinished,
                User = $"{_.UserDto.FirstName} {_.UserDto.LastName}",
            })
            .ToListAsync();
        return Ok(applications);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("accepted",Name = "List Accepted Applications")]
    public async Task<IActionResult> ListAcceptedApplications()
    {
        var applications = await _db.Applications
            .Where(_ => _.Status == Status.Accepted)
            .Select(_ => new ApplicationListItem
            {
                Id = _.Id,
                Make = _.Vehicle.Make,
                Model = _.Vehicle.Model,
                Status = _.Status,
                DateAdded = _.DateAdded,
                DateFinished = _.DateFinished,
                User = $"{_.UserDto.FirstName} {_.UserDto.LastName}",
            })
            .ToListAsync();
        return Ok(applications);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("waiting",Name = "List Waiting Applications")]
    public async Task<IActionResult> ListWaitingApplications()
    {
        var applications = await _db.Applications
            .Where(_ => _.Status == Status.Waiting)
            .Select(_ => new ApplicationListItem
            {
                Id = _.Id,
                Make = _.Vehicle.Make,
                Model = _.Vehicle.Model,
                Status = _.Status,
                DateAdded = _.DateAdded,
                DateFinished = _.DateFinished,
                User = $"{_.UserDto.FirstName} {_.UserDto.LastName}",
            })
            .ToListAsync();
        return Ok(applications);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("rejected",Name = "List Rejected Applications")]
    public async Task<IActionResult> ListRejectedApplications()
    {
        var applications = await _db.Applications
            .Where(_ => _.Status == Status.Rejected)
            .Select(_ => new ApplicationListItem
            {
                Id = _.Id,
                Make = _.Vehicle.Make,
                Model = _.Vehicle.Model,
                Status = _.Status,
                DateAdded = _.DateAdded,
                DateFinished = _.DateFinished,
                User = $"{_.UserDto.FirstName} {_.UserDto.LastName}",
            })
            .ToListAsync();
        return Ok(applications);
    }

    private Guid GetUserId()
    {
        var user = _accessor.HttpContext.User;
        var id = Guid.Parse(user.Claims.FirstOrDefault(_ => _.Type == "id").Value);
        return id;
    }

    private bool IsAdmin()
    {
        var user = _accessor.HttpContext.User;
        var role = user.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role);
        if (role is null) return false;
        return role.Value == "Admin";
    }

    private UserDto GetUser()
    {
        var user = _accessor.HttpContext.User;
        return new UserDto
        {
            FirstName = user.Claims.FirstOrDefault(_ => _.Type == "firstName").Value,
            LastName = user.Claims.FirstOrDefault(_ => _.Type == "lastName").Value
        };
    }
}
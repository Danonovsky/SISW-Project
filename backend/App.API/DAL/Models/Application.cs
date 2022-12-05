using System.ComponentModel.DataAnnotations.Schema;
using App.API.Models;

namespace App.API.DAL.Models;

public class Application
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Vehicle Vehicle { get; set; }
    public UserDto UserDto { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime? DateFinished { get; set; }
    public string? FinalRegistrationNumber { get; set; }
    public string? ReasonRejected { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    Waiting,
    Accepted,
    Rejected
}
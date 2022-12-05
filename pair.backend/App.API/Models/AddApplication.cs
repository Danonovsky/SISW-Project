using App.API.DAL.Models;

namespace App.API.Models;

public class AddApplication
{
    public Vehicle Vehicle { get; set; }
    public string? DesiredRegistrationNumber { get; set; }
}
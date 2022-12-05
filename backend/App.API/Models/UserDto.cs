using App.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace App.API.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
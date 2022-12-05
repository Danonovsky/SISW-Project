namespace App.API.DAL.Models;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string VIN { get; set; }
    public string RegistrationNumber { get; set; }
    public double EngineCapacity { get; set; }
}
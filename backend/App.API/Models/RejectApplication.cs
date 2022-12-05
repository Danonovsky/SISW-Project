namespace App.API.Models;

public class RejectApplication
{
    public Guid ApplicationId { get; set; }
    public string Reason { get; set; }
}
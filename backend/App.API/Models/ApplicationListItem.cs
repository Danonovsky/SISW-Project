using App.API.DAL.Models;

namespace App.API.Models;

public class ApplicationListItem
{
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string User { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime? DateFinished { get; set; }
    public Status Status { get; set; }
}
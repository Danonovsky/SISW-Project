namespace Identity.API.Models;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public bool Failed => !Succeeded;
    public int Code { get; set; }
    public List<Dictionary<string, string>> Messages { get; set; }
}

public class ApiResponse
{
    public bool Succeeded { get; set; }
    public bool Failed => !Succeeded;
    public int Code { get; set; }
    public List<Dictionary<string, string>> Messages { get; set; }
}
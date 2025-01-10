namespace ContactManagement.Application.Dtos;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
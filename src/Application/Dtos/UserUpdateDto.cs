namespace Application.Dtos;

public class UserUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Bio { get; set; }
}

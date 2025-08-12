namespace Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }

    public string? Address { get; set; }
    public string? Bio { get; set; }

    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? BanTime { get; set; }

    public long RoleId { get; set; }
    public UserRole Role { get; set; }

    public long? PatientId { get; set; }
    public Patient? Patient { get; set; }
    public long? DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public long? ConfirmerId { get; set; }
    public UserConfirmer? Confirmer { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<Notification> Notifications { get; set; }
}
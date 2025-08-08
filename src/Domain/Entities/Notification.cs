namespace Domain.Entities;

public class Notification
{
    public long Id { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long UserId { get; set; }
    public User User { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models;
//Deneme satırı
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Navigation property
    public virtual ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
} 
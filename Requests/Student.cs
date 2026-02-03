using System.ComponentModel.DataAnnotations;

namespace practice.Requests.Student;

public class UpdateStudentRequest
{
    public int Id { get; set; }
    public string? Login { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Phone { get; set; }
    [Required]
    public string? VkProfileLink { get; set; }
}
public class CreateStudentRequest
{
    public int Id { get; set; }
    public string? Login { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Phone { get; set; }
    [Required]
    public string? VkProfileLink { get; set; }
}

public class UploadAvatarRequest
{
    public IFormFile Avatar { get; set; }
}



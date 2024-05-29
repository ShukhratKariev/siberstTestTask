using System.ComponentModel.DataAnnotations;

namespace SibersTestTask.Models.Employees;

public class EditEmployeeForm
{
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string Surname { get; set; }
    [Required] [MaxLength(50)] public string Patronymic { get; set; }
    [MaxLength(50)] public string Email { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Core.Enums;
using Infrastructure.EntityFrameworkCore;

namespace SibersTestTask.Models.Projects;

public class EditProjectForm
{
    [Required] [MaxLength(50)] public string Name { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectPriorityEnum Priority { get; set; }

    [Required] [MaxLength(50)] public string ContractorCompanyName { get; set; }

    [Required] [MaxLength(50)] public string Client { get; set; }
}
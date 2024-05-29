using Core.Enums;

namespace Application.Dtos;

public class ProjectSearchArguments
{
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Client { get; set; }
    public string Contractor { get; set; }
    public ProjectPriorityEnum? Priority { get; set; }
}
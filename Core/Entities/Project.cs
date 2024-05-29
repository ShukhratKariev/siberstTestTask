using Core.Enums;

namespace Core;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ClientCompanyName { get; set; }
    public string ContractorCompanyName { get; set; }
    public ProjectPriorityEnum Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public List<ProjectEmployee> Employees { get; set; }
    public List<Ticket> Tickets { get; set; }
}
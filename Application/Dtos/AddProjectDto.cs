using Core;
using Core.Enums;

namespace Application.Dtos;

public class AddProjectDto
{
    public string ProjectName { get; set; }
    
    public string ClientCompanyName { get; set; }
    
    public string ContractorCompanyName { get; set; }
    
    public ProjectPriorityEnum Priority { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public List<int> EmployeeIds { get; set; }
    
    public int ProjectLeadId { get; set; }
    
}
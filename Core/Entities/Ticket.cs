using Core.Enums;

namespace Core;

public class Ticket
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public TicketProgressionEnum Status { get; set; }
    public TicketPriorityEnum Priority { get; set; }

    public int ProjectId { get; set; }
    public int AssigneeId { get; set; }
    public int CreatorId { get; set; }

    public Project Project { get; set; }
    public Employee Assignee { get; set; }
    public Employee Creator { get; set; }
}
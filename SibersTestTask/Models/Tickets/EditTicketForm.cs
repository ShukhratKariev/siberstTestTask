using Core;
using Core.Enums;

namespace SibersTestTask.Models.Tickets;

public class EditTicketForm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public TicketProgressionEnum Status { get; set; }
    public TicketPriorityEnum Priority { get; set; }
    public Employee Creator { get; set; }
    public Employee Assignee { get; set; } 
}
using Core;
using Core.Enums;

namespace Application.Dtos;

public class TicketSearchArguments
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public Employee? Creator { get; set; }
    public Employee? Assignee { get; set; }
    public TicketProgressionEnum? Status { get; set; }
    public string? Comment { get; set; }
    public TicketPriorityEnum? Priority { get; set; }
}